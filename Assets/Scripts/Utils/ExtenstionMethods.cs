using UnityEngine;

public static class ExtenstionMethods
{
    public static bool Contains(this LayerMask layermask, int layer)
    {
        return layermask == (layermask | (1 << layer));
    }

    public static Line2D? ContainsLineSegment(this Rect rect, Line2D line)
    {
        Vector2 rectLeftBottom, rectLeftTop, rectRightBottom, rectRightTop;
        rectLeftBottom = rect.min;
        rectLeftTop = new Vector2(rect.xMin, rect.yMax);
        rectRightBottom = new Vector2(rect.xMax, rect.yMin);
        rectRightTop = rect.max;

        Line2D rectLeftBorder = new Line2D(rectLeftBottom, rectLeftTop);
        Line2D rectRightBorder = new Line2D(rectRightBottom, rectRightTop);
        Line2D rectTopBorder = new Line2D(rectLeftTop, rectRightTop);
        Line2D rectBottomBorder = new Line2D(rectLeftBottom, rectRightBottom);

        Vector2? lineSegmentP1 = null, lineSegmentP2 = null;
        if (line.P1.x < rect.min.x && line.P2.x < rect.min.x ||
            line.P1.x > rect.max.x && line.P2.x > rect.max.x ||
            line.P1.y < rect.min.y && line.P2.y < rect.min.y ||
            line.P1.y > rect.max.y && line.P2.y > rect.max.y)
        {
            return null;
        }
        else
        {
            if (rect.Contains(line.P1))
            {
                lineSegmentP1 = line.P1;
            }
            if (rect.Contains(line.P2))
            {
                lineSegmentP2 = line.P2;
            }
            //if two line point are inside the rect
            if (IsFoundAllLineSegmentPoints())
            {
                return GetCompleteLineSegment();
            }

            if (TrySetLineSegment(rectLeftBorder))
            {
                return GetCompleteLineSegment();
            }
            if (TrySetLineSegment(rectRightBorder))
            {
                return GetCompleteLineSegment();
            }
            if (TrySetLineSegment(rectBottomBorder))
            {
                return GetCompleteLineSegment();
            }
            if (TrySetLineSegment(rectTopBorder))
            {
                return GetCompleteLineSegment();
            }
            return null;
        }

        Line2D GetCompleteLineSegment()
        {
            return new Line2D(lineSegmentP1.Value, lineSegmentP2.Value);
        }

        bool TrySetLineSegment(Line2D borderLine)
        {
            Vector2? intersectionPoint = borderLine.IntersectsWithLine(line);
            if (intersectionPoint.HasValue)
            {
                return (SetLineSegmentP1OrP2Value(intersectionPoint.Value));
            }
            return false;
        }

        bool SetLineSegmentP1OrP2Value(Vector2 value)
        {
            if (!lineSegmentP1.HasValue)
            {
                lineSegmentP1 = value;
            }
            else if (!lineSegmentP2.HasValue)
            {
                lineSegmentP2 = value;
            }
            return IsFoundAllLineSegmentPoints();
        }

        bool IsFoundAllLineSegmentPoints()
        {
            return lineSegmentP1.HasValue && lineSegmentP2.HasValue;
        }
    }

}
