using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Vector3Extensions
{
    public static Vector3 XoZ(this Vector3 value) => new Vector3(value.x, 0f, value.z);
    public static Vector3 ooZ(this Vector3 value) => new Vector3(0f, 0f, value.z);
    public static Vector3 ooZ_Rounded(this Vector3 value) => new Vector3(0f, 0f, Mathf.RoundToInt(value.z));
    public static Quaternion xyzw_custom_z(this Quaternion value) => new Quaternion(value.z, value.x, value.y, value.w);
}