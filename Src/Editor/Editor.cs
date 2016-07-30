using System;

namespace Octopus
{
	public partial class Editor : Gtk.Window
	{
		public Editor() :
			base(Gtk.WindowType.Toplevel)
		{
			this.Build();
		}
	}
}

