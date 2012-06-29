using System;
using System.Web;
using System.Web.UI;
using System.IO;

namespace WebDotNet
{
	public static class StringHelper
	{
		public static string RemoveLast(this string self, int count)
		{
			return self.Remove(self.Length - count, count);
		}
	}
	
	public class Default : System.Web.IHttpHandler
	{
		private static void WriteInstructions(TextWriter Script)
		{
			
			Script.Write("<script>(function(){\"use strict\";var cil = [];\n\n");
			foreach(var file  in Directory.EnumerateFiles(Path.Combine( Directory.GetCurrentDirectory() , "vm", "set")))
			{
				var part = file.Split('-')[0].Split('x');
				int code = Convert.ToInt32(part[part.Length - 1], 16);
				
				Script.WriteLine("//" + file.Split('-')[1].RemoveLast(3));
				
				Script.Write("cil[" + code + "] = function(push, pop, goto, locals, args, ret, assembly, operand)\n{\n");
				Script.Write("\tconsole.debug(\""+ file.Split('-')[1].RemoveLast(3) + "\\t\" + operand)\n");
				var s = new StreamReader(File.OpenRead(file));
				while(!s.EndOfStream)
				{
					Script.WriteLine("\t" + s.ReadLine());
				}
				
				Script.Write("\n};\n");
			}
			
			Script.Write(new StreamReader(File.OpenRead(Path.Combine( Directory.GetCurrentDirectory() , "vm", "runtime.js") )).ReadToEnd());
			
			Script.Write("\n}());</script>");
		}
		
		
		public virtual bool IsReusable {
			get {
				return false;
			}
		}
		
		public virtual void ProcessRequest (HttpContext context)
		{
			WriteInstructions(context.Response.Output);
		}
	}
}

