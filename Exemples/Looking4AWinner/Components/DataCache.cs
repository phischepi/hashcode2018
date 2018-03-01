using System.Linq;

namespace Looking4AWinner.LikeUs.Components
{
    public class DataCache : VideoPublisher
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DataCache" /> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="size">The size.</param>
        public DataCache(int id, int size)
            : base(id, size)
        {
        }

        public override string ToString()
        {
            return string.Format("{0} {1}", Id, string.Join(" ", Videos.Select(v => v.Id)));
        }
    }
}