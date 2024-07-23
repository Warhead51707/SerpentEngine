using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerpentEngine;
public abstract class ImGuiDrawable
{
    public bool Visible { get; set; } = true;
    public abstract void Draw();
}
