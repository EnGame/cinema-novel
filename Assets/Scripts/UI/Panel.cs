using System.Collections.Generic;
using UnityEngine;

namespace Nashim.UI
{
    public class Panel : MonoBehaviour
    {
        #region Events
        public delegate void EnableAction();
        public event EnableAction OnEnabled;

        public delegate void DisableAction();
        public event EnableAction OnDisabled;

        public delegate void OnUpdateTick();
        public event OnUpdateTick onUpdateTick;

        public delegate void OnFixedTick();
        public event OnFixedTick onFixedTick;
        #endregion

        public List<PanelObject> panelObjects = new List<PanelObject>();

        public Dictionary<string, object> panelVariables = new Dictionary<string, object>();
        public bool hideAfterAnim = true;

        void OnEnable()
        {
            OnEnabled?.Invoke();
        }
        void OnDisable()
        {
            OnDisabled?.Invoke();
        }
        private void Update()
        {
            onUpdateTick?.Invoke();
        }
        private void FixedUpdate()
        {
            onFixedTick?.Invoke();
        }

        #region Main
        /// <summary>
        /// Return GameObject by key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public GameObject GetObjectByKey(string key)
        {
            if (key == "") return null;

            PanelObject panelObjectTemp = new PanelObject();
            foreach (PanelObject panelObject in panelObjects)
            {
                if (panelObject.Key == key)
                {
                    panelObjectTemp = panelObject;
                    break;
                }
            }

            if (panelObjectTemp.Object == null)
            {
                Debug.LogError(string.Format("ERROR::PANEL_KEY_OR_OBJECT_IS_EMPTY: {0}", key));
            }

            return panelObjectTemp.Object;
        }

        /// <summary>
        /// Return Component T by key.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T GetComponentByKey<T>(string key)
        {
            T component = default;
            if (GetObjectByKey(key).GetComponent<T>() != null) component = GetObjectByKey(key).GetComponent<T>();

            return component;
        }

        /// <summary>
        /// Add or change "PanelVariables".
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void SetPanelVariable(string key, object value)
        {
            if (panelVariables.ContainsKey(key))
                panelVariables[key] = value;
            else
                panelVariables.Add(key, value);
        }
        #endregion
    }

    [System.Serializable]
    public struct PanelObject
    {
        public string Key;
        public GameObject Object;
        public PanelType type;

        public enum PanelType
        {
            Text,
            Image,
            Dropdown,
            Button,
            Toggle,
            ProgressBar,
            Panel,
            Layout,
            Slider
        }
    }
}