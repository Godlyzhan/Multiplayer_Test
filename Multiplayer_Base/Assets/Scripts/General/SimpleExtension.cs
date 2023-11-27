using System;
using UnityEngine;
using UnityEngine.InputSystem;

public static class SimpleExtension
{
    public static string Cyan(this string text) => $"<color=cyan>{text}</color>";
    public static string Cyan(this int text) => $"<color=cyan>{text}</color>";
    public static string Cyan(this float text) => $"<color=cyan>{text}</color>";
    public static string Red(this string text) => $"<color=FF3A0F>{text}</color>";
    public static string Red(this int text) => $"<color=FF3A0F>{text}</color>";
    public static string Red(this float text) => $"<color=FF3A0F>{text}</color>";
    public static string Green(this string text) => $"<color=13FF5B>{text}</color>";
    public static string Green(this int text) => $"<color=13FF5B>{text}</color>";
    public static string Green(this float text) => $"<color=13FF5B>{text}</color>";
    public static string Pink(this string text) => $"<color=F413FF>{text}</color>";
    public static string Pink(this int text) => $"<color=F413FF>{text}</color>";
    public static string Pink(this float text) => $"<color=F413FF>{text}</color>";
}

public static class Message
{

}
