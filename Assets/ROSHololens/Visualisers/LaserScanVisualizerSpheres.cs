/*
© Siemens AG, 2018
Author: Berkay Alp Cakal (berkay_alp.cakal.ct@siemens.com)

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at
<http://www.apache.org/licenses/LICENSE-2.0>.
Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/

using UnityEngine;

namespace ROSHololens
{

    public class LaserScanVisualizerSpheres : LaserScanVisualizer
    {
        [Range(0.01f, 0.1f)]
        public float objectWidth;

        private GameObject[] LaserScan;
        private bool IsCreated = false;
        

        private void Create(int numOfSpheres)
        {
            LaserScan = new GameObject[numOfSpheres];

            for (int i = 0; i < numOfSpheres; i++)
            {
                //Debug.Log("creating sphere");
                LaserScan[i] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                DestroyImmediate(LaserScan[i].GetComponent<Collider>());
                LaserScan[i].name = "LaserScanSpheres";
                LaserScan[i].transform.parent = transform;
                LaserScan[i].GetComponent<Renderer>().material = new Material(Shader.Find("Particles/Additive"));
                LaserScan[i].SetActive(false);

            }
            IsCreated = true;
        }

        protected override void Visualize()
        {
            UnityEngine.Profiling.Profiler.BeginSample("Laser");
            
            if (!IsCreated)
                Create(directions.Length);
            //Debug.Log("update");
            for (int i = 0; i < directions.Length; i++)
            {
                //Debug.Log(ranges[i]);
                if (float.IsInfinity(ranges[i]))
                {
                    LaserScan[i].SetActive(false);
                }
                else
                {
                    LaserScan[i].SetActive(true);
                    LaserScan[i].GetComponent<Renderer>().material.SetColor("_TintColor", GetColor(ranges[i]));
                    LaserScan[i].transform.localScale = objectWidth * Vector3.one;
                    LaserScan[i].transform.localPosition = ranges[i] * directions[i];
                }
            }
            UnityEngine.Profiling.Profiler.EndSample();
        }

        protected override void DestroyObjects()
        {
            if (LaserScan != null) {
                for (int i = 0; i < LaserScan.Length; i++)
                    Destroy(LaserScan[i]);
                IsCreated = false;
            }
        }

    }
}
