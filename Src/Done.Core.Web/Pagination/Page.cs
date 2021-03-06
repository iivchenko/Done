﻿namespace Done.Core.Web.Pagination
{
    public sealed class Page
    {
        public Page(int index, bool isActive, bool isEnabled)
        {
            Index = index;
            IsActive = isActive;
            IsEnabled = isEnabled;
        }

        public int Index { get; }

        public bool IsActive { get; }

        public bool IsEnabled { get; }
    }
}