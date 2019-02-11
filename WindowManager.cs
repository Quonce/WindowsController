using DebugOnScreen;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WindowManager
{
    public class Window : MonoBehaviour
    {
        private static Dictionary<string, Animator> inactiveWindows = new Dictionary<string, Animator>();
        private static Dictionary<string, Animator> activeWindows = new Dictionary<string, Animator>();

        private static bool canCloseLast;

        public static void Init(bool AtLeastOneWindowAlwaysActive)
        {
            canCloseLast = !AtLeastOneWindowAlwaysActive;
            Animator[] windows = FindObjectsOfType<Animator>();

            inactiveWindows.Clear();
            for (int i = 0; i < windows.Length; i++)
            {
                if (inactiveWindows.ContainsKey(windows[i].name))
                {
                    Debuger.LogError($"Window with name {windows[i].name} already exists!! Please rename one of this windows.");
                    return;
                }
                inactiveWindows.Add(windows[i].name, windows[i]);
            }
        }

        public static void Open(string name)
        {
            if (inactiveWindows.ContainsKey(name))
            {
                if (activeWindows.ContainsKey(name))
                {
                    Debuger.LogError($"Window {name} appears in both Dictionarys!! WTF??? CHECK YOUR CODE!!! Do not add windows manually!");
                }
                else
                {
                    activeWindows.Add(name, inactiveWindows[name]);
                    inactiveWindows.Remove(name);

                    activeWindows[name].SetTrigger("Open");
                }
            }
            else
            {
                if (!activeWindows.ContainsKey(name))
                    Debuger.LogError($"There is no window with name {name}!! Please check name of window you call.");
                else
                    Debuger.Log($"Window {name} is already active!");
            }
        }

        public static void Close(string name)
        {
            if (activeWindows.ContainsKey(name))
            {
                if (inactiveWindows.ContainsKey(name))
                {
                    Debuger.LogError($"Window {name} appears in both Dictionarys!! WTF??? CHECK YOUR CODE!!! Do not add windows manually!");
                }
                else
                {
                    inactiveWindows.Add(name, activeWindows[name]);
                    activeWindows.Remove(name);

                    inactiveWindows[name].SetTrigger("Close");
                }
            }
            else
            {
                if (!inactiveWindows.ContainsKey(name))
                    Debuger.LogError($"There is no window with name {name}!! Please check name of window you call.");
                else
                    Debuger.Log($"Window {name} is already inactive!");
            }
        }

        public static bool isActive(string name)
        {
            if (activeWindows.ContainsKey(name))
            {
                return true;
            }
            else
            {
                if (inactiveWindows.ContainsKey(name))
                    return false;
                else
                {
                    Debuger.LogError($"There is no window with name {name}!! Please check name of window you call.");
                    return false;
                }
            }
        }

        public static void CloseLast()
        {
            if (canCloseLast || activeWindows.Count > 1)
                Close(activeWindows.Keys.ElementAt(activeWindows.Count - 1));
        }
    }
}