using System.Collections.Immutable;
using System.Xml.Schema;
using Microsoft.VisualBasic;

namespace AOC.Y2024
{
    public class Day23 : IProblem
    {
        private Dictionary<string, List<string>> links = new Dictionary<string, List<string>>();
        private HashSet<string> systems = new HashSet<string>();

        public void Parse(string[] input)
        {
            foreach (var line in input)
            {
                var link = line.Split('-');

                if (!links.ContainsKey(link[0])) links[link[0]] = new List<string>();
                if (!links.ContainsKey(link[1])) links[link[1]] = new List<string>();

                links[link[0]].Add(link[1]);
                links[link[1]].Add(link[0]);

                systems.Add(link[0]);
                systems.Add(link[1]);
            }

            foreach (var (_, linkList) in links) linkList.Sort();
        }

        public object PartOne()
        {
            return CountTrios((a, b, c) => a[0] == 't' || b[0] == 't' || c[0] == 't');
        }

        public object PartTwo()
        {
            return BronKerbosch(systems.ToImmutableList())
                .Select(c => c.Sort())
                .Select(c => string.Join(',', c))
                .ToList()
                .MaxBy(c => c.Length) ?? "???";
        }

        public int CountTrios(Func<string, string, string, bool> validFunc)
        {
            var count = 0;

            var systemsList = systems.ToList();
            for (int i = 0; i < systems.Count; i++)
                for (int j = i + 1; j < systems.Count; j++)
                    for (int k = j + 1; k < systems.Count; k++)
                    {
                        if (i == j || i == k || j == k) continue;

                        string a = systemsList[i];
                        string b = systemsList[j];
                        string c = systemsList[k];

                        if (validFunc(a, b, c) && links[a].Contains(b) && links[a].Contains(c) && links[b].Contains(c)) count++;
                    }

            return count;
        }

        public ImmutableList<string> BuildLargestClique(ImmutableList<string> otherSystems)
        {
            var largest = ImmutableList<string>.Empty;

            foreach (var system in links[otherSystems.First()])
            {
                if (otherSystems.Contains(system)) continue;
                if (!otherSystems.All(o => links[system].Contains(o))) continue;

                var newClique = otherSystems.Add(system);
                var largerClique = BuildLargestClique(newClique);

                if (largerClique.Count > largest.Count) largest = largerClique;
                else if (newClique.Count > largest.Count) largest = newClique;
            }

            return largest;
        }

        public List<string> FindLargestClique()
        {
            List<string> largest = new List<string>();
            foreach (var (system, connections) in links)
            {
                //if (connections.Count < largest.Count) continue;

                List<string> connected = new List<string>(connections);
                foreach (var left in connections)
                {
                    if (!connected.Contains(left)) continue;

                    foreach (var right in connections)
                    {
                        // strings sorted, so skip ones that have already been checked
                        if (string.Compare(left, right) <= 0 || !connected.Contains(right)) continue;
                        if (!links[left].Contains(right)) connected.Remove(right);
                    }
                }

                if (connected.Count + 1 > largest.Count)
                {
                    largest = connected;
                    largest.Add(system);
                }
            }

            largest.Sort();
            return largest;
        }

        public IEnumerable<ImmutableList<string>> BronKerbosch(ImmutableList<string> nodes)
        {
            return BronKerbosch(nodes, ImmutableList<string>.Empty, ImmutableList<string>.Empty);
        }

        public IEnumerable<ImmutableList<string>> BronKerbosch(ImmutableList<string> r, ImmutableList<string> p, ImmutableList<string> x)
        {
            if (r.Count == 0 && x.Count == 0) yield return p;

            foreach (var n in r)
            {
                var np = p.Add(n);
                var nr = r.Where(n => links[n].Contains(n)).ToImmutableList();
                var nx = x.Where(n => links[n].Contains(n)).ToImmutableList();
                foreach (var clique in BronKerbosch(np, nr, nx)) yield return clique;

                r = r.Remove(n);
                x = x.Remove(n);
            }
        }
    }
}