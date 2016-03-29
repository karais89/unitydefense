using UnityEngine;
using System.Collections;

namespace MapEditor
{
    public class BuildScrollModel : MonoBehaviour
    {
        public enum eBuildType
        {
            None = 0,
            Tile,
            Rock,
            Tree,
            Max
        }

        private eBuildType m_eBuildType;

        public eBuildType EBuildType
        {
            get
            {
                return m_eBuildType;
            }

            set
            {
                m_eBuildType = value;
            }
        }
    }
}

