using SatisfactoryTree.Logic.Models;

namespace SatisfactoryTree.Web.Services;

public interface IFactoryItemDisplayService
{
    string GetPartImagePath(string partName);
    string GetBuildingImagePath(string buildingName);
    string GetBuildingName(string buildingName);
    bool HasBuildingImage(string buildingName);
    string GetPartDisplayName(Part part);
    bool GetPartIsFluid(Part part);
    bool GetPartIsFicsmas(Part part);
}