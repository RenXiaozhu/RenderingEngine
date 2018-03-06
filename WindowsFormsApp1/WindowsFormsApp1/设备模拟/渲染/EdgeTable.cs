using System;
namespace RenderingEngine
{
    public class EdgeTable
    {
        public EdgeTable()
        {
            
        }

        public static void SearchTable(int index, Edge[] ET , Edge AEL)
        {
            // 将边表的边插入活动边表，并删除边表里的边
            while (ET[index].nextEdge != null)
            {
                InsertEdge(ref AEL.nextEdge, ET[index].nextEdge);
                ET[index].nextEdge = ET[index].nextEdge.nextEdge;
            }
        }

        public static void InsertEdge(ref Edge root, Edge e)
        {
            Edge newEdge = (Edge)e.Clone();
            Edge previous;
            Edge current;

            current = root;
            previous = null;

            // 将 在链表中的边按 x 从小到大 排序
            while (current != null && (current.x < newEdge.x || (current.x == newEdge.x && current.deltaX < newEdge.deltaX)))
            {
                //保存当前链表位置
                previous = current;

                current = current.nextEdge;
            }

            newEdge.nextEdge = current;

            if (previous == null)
                root = newEdge;
            else
                previous.nextEdge = newEdge;
        }

        public static void DeleteEdge(Edge root, int i)
        {
            Edge p = root;
            while (p.nextEdge != null)
            {
                // 如果扫描线当前位置已经超出边的最大Y值 ,则删除这条边
                if (p.nextEdge.yMax - 1 == i)
                {
                    Edge pDelete = p.nextEdge;
                    p.nextEdge = pDelete.nextEdge;
                    pDelete.nextEdge = null;
                }
                else
                {
                    p = p.nextEdge;
                }
            }

            p = root;

            // x' = x + 1/m; 表示的是扫描线与边的交点
            while (p.nextEdge != null)
            {
                p.nextEdge.x += p.nextEdge.deltaX;
                p = p.nextEdge;
            }
        }
    }
}
