// -----------------------------------------------------------------------
// <copyright file="CommonUtil.cs" company="1-system-group">
// Copyright (c) 1-system-group. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Diary_Sample.Common
{
    public class CommonUtil
    {
        private const int MaxIdValue = 16777215;
        public static bool CheckId(string id)
        {
            if (id == null)
            {
                return false;
            }
            int i;
            bool ret = int.TryParse(id, out i);
            if (!ret)
            {
                return false;
            }
            if (i >= MaxIdValue)
            {
                return false;
            }

            return true;
        }
    }
}
