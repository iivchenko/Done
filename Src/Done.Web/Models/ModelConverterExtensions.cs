using Done.Domain;
using Done.Web.Models.Goals;
using System;

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

        public static Goal ToModel(this NewGoalViewModel viewModel)
        {
            var date = DateTime.UtcNow;

            return new Goal
            {
                Name = viewModel.Name,
                Description = viewModel.Description,
                State = State.Open,
                CreationDate = date,
                ModificationDate = date
            };
        }
        public static Goal ToModel(this EditGoalViewModel viewModel)
        {
            var date = DateTime.UtcNow;

            return new Goal
            {
                Id = viewModel.Id,
                Name = viewModel.Name,
                Description = viewModel.Description,
                ModificationDate = date
            };
        }

        public static EditGoalViewModel ToEditViewModel(this Goal model)
        {
            return new EditGoalViewModel
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                State = model.State
            };
        }
    }
}