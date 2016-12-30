using System.Collections.Generic;

namespace Shared
{
    public class EEGSet
    {
        #region BasicInfo

        public int id;
        private string Name;
        private string Description;

        #endregion BasicInfo

        private List<EEGRecording> recordings;
    }
}