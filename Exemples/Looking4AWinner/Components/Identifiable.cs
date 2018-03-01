#region Imports

using Gilgen.Utils.Common.ModuleBase.Interfaces;

#endregion

namespace Looking4AWinner.LikeUs.Components {
    public abstract class Identifiable : IIdentifiable {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the identifier.
        /// </summary>
        /// <value>
        ///     The identifier.
        /// </value>
        public int Id { get; set; }

        #endregion
    }
}