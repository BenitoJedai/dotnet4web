/*

This file is part of dotnet4web.

net.js is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as
published by the Free Software Foundation, either version 3 of the
License, or (at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Affero General Public License for more details.

You should have received a copy of the GNU Affero General Public License
along with this program.  If not, see `<http://www.gnu.org/licenses/>`.

*/
using System;
using Org.W3C;
using Org.W3C.XMLHttpRequest;
using Org.W3C.HTML;
using Org.W3C.Events;

namespace Net.Js.Example {
    public class Index: Window {
        public void OnAJAXButtonClick() {
            var xhr = new XMLHttpRequest();
            xhr.Open("GET", "example.html");
            xhr.Send();
            xhr.OnReadyStateChange += delegate() {
                if (xhr.ReadyState == ReadyState.Done) {

                    var pre = Document.CreateElement("pre");
                    pre.AppendChild(Document.CreateTextNode(xhr.ResponseText));
                    Alert(xhr.ResponseText);

                }
            };
        }
    }

    public static class Pruebita {
        public static int lala(Index jajaja) {
            return 0;
        }

        public static Index lele(Index jajaja) {
            return null;
        }
    }

	/*public class generica<T>
	{
		public void lala(T p1) { }
		public void jajajaja<T2>() {
		}
	}*/

}