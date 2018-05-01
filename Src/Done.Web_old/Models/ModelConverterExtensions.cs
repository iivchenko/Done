using Done.Domain;
using Done.Web.Models.Goals;

namespace Done.Web.Models
{
    public static class ModelConverterExtensions
    {
        public static Goal ToModel(this GoalViewModel viewModel)
        {
            return new Goal
            {
                Id = viewModel.Id,
                Name = viewModel.Name,
                Description = viewModel.Description,
                State = viewModel.State
            };
        }

        public static GoalViewModel ToViewModel(this Goal model)
        {
            return new GoalViewModel()
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                State = model.State
            };
        }
    }
}