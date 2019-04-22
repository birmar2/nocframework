using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DrawingModule._interfaces
{
    public interface IDijkstra
    {
        List<int> DijkstraAlg(int[,] graph, int sourceNode, int destinationNode);
    }
}
