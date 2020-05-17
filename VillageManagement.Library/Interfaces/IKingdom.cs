using System;
using System.Collections.Generic;
using System.Text;

namespace VillageManagement.Library.Interfaces
{
    public interface IInhabitant
    {
        /// <summary>
        /// Name property.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Calculate taxes of the IInhabitant.
        /// </summary>
        /// <param name="taxesValue">Should be the current base taxes value.</param>
        /// <returns>Complete taxes of the IInhabitant.</returns>
        double Taxes(double taxesValue);
    }

    public interface IChief : IInhabitant
    {
        /// <summary>
        /// Date as year number.
        /// </summary>
        int ChiefSince { get; }
    }

    public interface IManagement
    {
        /// <summary>
        /// Calculate taxes of all IInhabitants.
        /// </summary>
        /// <returns></returns>
        double TotalTaxes();

        /// <summary>
        /// Base taxes of a item.
        /// </summary>
        double BaseTaxes { set; }

        /// <summary>
        /// Gets all objects which implements IInhabitants.
        /// </summary>
        /// <returns>List with all IInhabitants.</returns>
        List<IInhabitant> AllInhabitants();

        /// <summary>
        /// Gets all objects which implements IChief.
        /// </summary>
        /// <returns>List with all IChiefs.</returns>
        List<IChief> AllChiefs();
    }
}
