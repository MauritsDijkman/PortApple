using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    public List<LineSegment> _lines;

    class Level1
    {
        public Level1() : base()
        {
            _lineContainer = new Canvas(width, height);
            AddChild(_lineContainer);

            _lines = new List<LineSegment>();
        }


        //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
        //                                                                      GetNumberOfLines()                                                                   //
        //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
        public int GetNumberOfLines()
        {
            return _lines.Count;
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
        //                                                                      LineSegment GetLine(int index)                                                       //
        //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
        public LineSegment GetLine(int index)
        {
            if (index >= 0 && index < _lines.Count)
            {
                return _lines[index];
            }
            return null;
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
        //                                                                      DrawLine(Vec2 start, Vec2 end)                                                       //
        //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
        public void DrawLine(Vec2 start, Vec2 end)
        {
            _lineContainer.graphics.DrawLine(Pens.White, start.x, start.y, end.x, end.y);
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
        //                                                                      AddLine(Vec2 start, Vec2 end)                                                        //
        //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
        public void AddLine(Vec2 start, Vec2 end, LineSegment l = null)
        {
            LineSegment line = l;

            if (l == null)
            {
                line = new LineSegment(start, end, 0xff00ff00, 4);  // Green line
                                                                    //line = new LineSegment(start, end, 0x0000ff00, 4);  // Invisible line
            }

            AddChild(line);
            _lines.Add(line);
            _ball.Add(new Ball(0, 0, 0, 0, start, moving: false));
            _ball.Add(new Ball(0, 0, 0, 0, end, moving: false));
        }
    }
}
