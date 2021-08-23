using UnityEngine;
#if UNITY_EDITOR
#endif

[System.Serializable]
public struct Line2D
{
    public Vector2 P1
    {
        get
        {
            return p1;
        }
        set
        {
            p1 = value;
            CalculateLineEquation();
        }
    }

    public Vector2 P2
    {
        get
        {
            return p2;
        }
        set
        {
            p2 = value;
            CalculateLineEquation();
        }
    }

    public float m { get; private set; }
    public float b { get; private set; }

    private Vector2 p1, p2;
    private bool isVerticalLine;

    public Line2D(Vector2 P1, Vector2 P2)
    {
        isVerticalLine = P1.x == P2.x;

        if (isVerticalLine)
        {
            if(P2.y > P1.y)
            {
                this.p1 = P1;
                this.p2 = P2;
            }
            else
            {
                this.p1 = P2;
                this.p2 = P1;
            }
        }
        else
        {
            if (P2.x > P1.x)
            {
                p1 = P1;
                this.p2 = P2;
            }
            else
            {
                this.p1 = P2;
                this.p2 = P1;
            }
        }
        m = (P2.y - P1.y) / (P2.x - P1.x);
        //y = mx + b
        //b = y - mx
        b = P1.y - (m * P1.x);

    }

    public Vector2? IntersectsWithLine(Line2D line)
    {
        //if lines are parallel or both vertical
        if (line.m == this.m ||
            (line.isVerticalLine && this.isVerticalLine))
        {
            return null;
        }
        float x, y;
        if (line.isVerticalLine && this.isVerticalLine == false)
        {
            x = line.p1.x;
            y = GetLineY(x);
        }
        else if (this.isVerticalLine && line.isVerticalLine == false)
        {
            x = this.p1.x;
            y = line.GetLineY(x);
        }
        else
        {
            //intersection x = (b2 - b1) / (m1 - m2)
            x = (line.b - this.b) / (this.m - line.m);
            y = GetLineY(x);
        }
        Vector2 intersectionPoint = new Vector2(x, y);
        if (IsPointOnLine(intersectionPoint) && line.IsPointOnLine(intersectionPoint))
        {
            return intersectionPoint;
        }
        else
        {
            return null;
        }
    }

    public Vector2 GetPointOnLine(float fraction)
    {
        fraction = Mathf.Clamp01(fraction);
        float x, y;
        if (isVerticalLine)
        {
            x = p1.x;
            y = Mathf.Lerp(p1.y, p2.y, fraction);
        }
        else
        {
            x = Mathf.Lerp(p1.x, p2.x, fraction);
            y = GetLineY(x);
        }
        return new Vector2(x, y);
    }

    private void CalculateLineEquation()
    {
        m = (p2.y - p1.y) / (p2.x - p1.x);
        //y = mx + b
        //b = y - mx
        b = p1.y - (m * p1.x);
    }

    private float GetLineY(float x)
    {
        if (isVerticalLine)
        {
            return p1.y;
        }
        else
        {
            return (m * x) + b;

        }
    }

    private bool IsInYRange(float y)
    {
        if (isVerticalLine)
        {
            return y >= p1.y && y <= p2.y;
        }
        else
        {
            return !(y > p1.y && y > p2.y || y < p1.y && y < p2.y);
        }
    }

    private bool IsInXRange(float x)
    {
        if (isVerticalLine)
        {
            return x == p1.x;
        }
        else
        {
            return !(x > p2.x || x < p1.x);
        }
    }

    private bool IsPointOnLine(Vector2 point)
    {
        if (isVerticalLine)
        {
            return (point.x == p1.x && IsInYRange(point.y));
        }
        else
        {
            float onLineY = GetLineY(point.x);
            return onLineY == point.y && IsInXRange(point.x);
        }
    }


}
