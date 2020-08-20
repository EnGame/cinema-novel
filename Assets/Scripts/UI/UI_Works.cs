using System;
using System.Collections.Generic;
using UnityEngine;

namespace Nashim.UI
{
    public abstract class UI_Works : MonoBehaviour
    {
        /// <summary>
        /// Active Panels in viewport
        /// </summary>
        public static List<Panel> viewportPanels = new List<Panel>();

        /// <summary>
        /// Add panel to viewport and enable
        /// </summary>
        /// <param name="panel">Panel Object</param>
        public static Panel AddPanelsToViewport(Panel panel)
        {
            viewportPanels.Add(panel);
            panel.gameObject.SetActive(true);
            return panel;
        }

        /// <summary>
        /// Add list of panels to viewport and enable
        /// </summary>
        /// <param name="panels">List on panel object</param>
        public static void AddPanelsToViewport(List<Panel> panels)
        {
            foreach (Panel panel in panels)
            {
                if (HasPanel(panel))
                {
                    Debug.Log($"Panel {panel.name} is already actived");
                }
                else
                {
                    Debug.Log($"Panel {panel.name} added to viewport");
                    viewportPanels.Add(panel);
                    panel.gameObject.SetActive(true);
                }
            }
        }

        /// <summary>
        /// Remove panel from viewport and disable
        /// </summary>
        /// <param name="panel">Panel Object</param>
        public static void RemovePanelFromViewport(Panel panel)
        {
            if (!HasPanel(panel))
            {
                Debug.Log(string.Format("ERROR::ACTIVE_PANELS_LIST_HAVE_NO {0}", panel.gameObject.name));
            }
            else
            {
                viewportPanels.Remove(panel);
                panel.gameObject.SetActive(false);
            }
        }

        /// <summary>
        /// Remove list of panels to viewport and enable
        /// </summary>
        /// <param name="panels">List on panel object</param>
        public static void RemovePanelsFromViewport(List<Panel> panels)
        {
            foreach (Panel panel in panels)
            {
                if (viewportPanels.Contains(panel))
                {
                    viewportPanels.Remove(panel);
                    panel.gameObject.SetActive(false);
                }
                else
                {
                    Debug.LogError(string.Format("ERROR::ACTIVE_PANELS_LIST_HAVE_NO {0}", panel.gameObject.name));
                }
            }
        }

        /// <summary>
        /// Remove all panels from viewport and disable
        /// </summary>
        public static void ClearViewport()
        {
            foreach (Panel panel in viewportPanels)
            {
                panel.gameObject.SetActive(false);
            }

            viewportPanels.Clear();
        }

        /// <summary>
        /// Get panel from active panels list by name
        /// </summary>
        /// <param name="name">Name of panel</param>
        /// <returns>Panel</returns>
        public static Panel GetActivePanelByName(string name)
        {
            Panel panelTemp = null;
            foreach (Panel panel in viewportPanels)
            {
                if (panel.name == name)
                {
                    panelTemp = panel;
                }
            }
            if (panelTemp == null)
            {
                Debug.LogError(string.Format("ERROR::ACTIVE_PANELS_LIST_HAVE_NO {0}", name));
            }
            return panelTemp;
        }

        /// <summary>
        /// Fill selected layout with objects
        /// </summary>
        /// <param name="layout">Vertical Layout</param>
        /// <param name="prefab">Object</param>
        /// <param name="countOfObjects">Count of objects</param>
        /// <param name="action">Action which must start</param>
        public static void FillLayout(Transform layout, GameObject prefab, int countOfObjects, Action<Panel> action = null)
        {
            for (int i = 0; i < countOfObjects; i++)
            {
                Panel panel = Instantiate(prefab, layout).GetComponent<Panel>();
                if (action != null)
                    action(panel);
            }
        }

        /// <summary>
        /// Add single GameObject (With Panel Component) to layout (Container).
        /// </summary>
        /// <param name="layout"></param>
        /// <param name="prefab"></param>
        /// <param name="action"></param>
        public static void AddPanelToLayout(Transform layout, GameObject prefab, Action<Panel> action = null)
        {
            Panel panel = Instantiate(prefab, layout.transform).GetComponent<Panel>();

            if (action != null)
                action(panel);
        }

        /// <summary>
        /// Clear all childs of Layout (Container).
        /// </summary>
        /// <param name="layout"></param>
        public static void ClearLayout(Transform layout)
        {
            foreach (Transform child in layout)
            {
                Destroy(child.gameObject);
            }
        }

        /// <summary>
        /// Check panel is Active by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool HasPanel(string name)
        {
            foreach (Panel panel in viewportPanels)
            {
                if (panel.name == name)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Check panel is Active
        /// </summary>
        /// <param name="panel"></param>
        /// <returns></returns>
        public static bool HasPanel(Panel panel)
        {
            foreach (Panel panels in viewportPanels)
            {
                if (panels == panel)
                {
                    return true;
                }
            }
            return false;
        }
    }
}