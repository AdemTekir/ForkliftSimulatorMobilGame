using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ForkliftController))]
public class ForkliftControllerEditor : Editor
{
    bool showWheelColliders;
    bool showWheelTransforms;

    bool showCenterOfMassPosition;
    bool editCenterOfMassPosition = true;

    bool motorSettings;

    bool gearSystem = true;
    bool showGearSystem;

    bool fuelSystem = true;
    bool showFuelSystem;

    //[ExecuteInEditMode]
    public override void OnInspectorGUI()
    {
        ForkliftController forkliftController = (ForkliftController)target;

        //------------ FOLDOUT FONT STYLE ------------\\
        GUIStyle foldoutStyle = EditorStyles.foldout;
        foldoutStyle.alignment = TextAnchor.MiddleLeft;
        foldoutStyle.fontSize = 13;
        foldoutStyle.fontStyle = FontStyle.Bold;
        foldoutStyle.normal.textColor = Color.white;

        //----------------------------------------------------------------------  WHEEL COLLIDER ----------------------------------------------------------------------------------------------------\\

        showWheelColliders = EditorGUILayout.BeginFoldoutHeaderGroup(showWheelColliders, "Wheel Colliders", foldoutStyle);
        EditorGUILayout.Space(3);

        if (showWheelColliders)
        {
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(serializedObject.FindProperty("frontRightWheel"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("frontLeftWheel"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("backRightWheel"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("backLeftWheel"));
            EditorGUI.indentLevel--;
        }
        EditorGUILayout.EndFoldoutHeaderGroup();
        EditorGUILayout.Space();

        //----------------------------------------------------------------------  WHEEL TRANSFORMS --------------------------------------------------------------------------------------------------\\

        showWheelTransforms = EditorGUILayout.BeginFoldoutHeaderGroup(showWheelTransforms, "Wheel Transforms", foldoutStyle);
        EditorGUILayout.Space(3);

        if (showWheelTransforms)
        {
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(serializedObject.FindProperty("frontRightWheelTransform"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("frontLeftWheelTransform"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("backRightWheelTransform"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("backLeftWheelTransform"));
            EditorGUI.indentLevel--;
        }
        EditorGUILayout.EndFoldoutHeaderGroup();
        EditorGUILayout.Space();
        
        //---------------------------------------------------------------------- MOTOR SETTINGS -----------------------------------------------------------------------------------------------------\\

        motorSettings = EditorGUILayout.BeginFoldoutHeaderGroup(motorSettings, "Motor Settings", foldoutStyle);
        EditorGUILayout.Space(3);

        if (motorSettings)
        {
            EditorGUI.indentLevel++;
            EditorGUILayout.LabelField("Car Speed", forkliftController.carSpeed.ToString("F2"));
            forkliftController.carSpeedCurve = EditorGUILayout.CurveField(forkliftController.carSpeedCurve, GUILayout.Height(100));
            if (forkliftController.carSpeedHasChanged != forkliftController.carSpeed)
            {
                forkliftController.carSpeedCurve.AddKey(Time.time, forkliftController.carSpeed);
                forkliftController.carSpeedHasChanged = forkliftController.carSpeed;
            }
            
            EditorGUILayout.PropertyField(serializedObject.FindProperty("acceleration"));
            forkliftController.carAccelerationCurve = EditorGUILayout.CurveField(forkliftController.carAccelerationCurve, GUILayout.Height(100));
            if (forkliftController.accelerationHasChanged != forkliftController.acceleration)
            {
                forkliftController.carAccelerationCurve.AddKey(Time.time, forkliftController.acceleration);
                forkliftController.accelerationHasChanged = forkliftController.acceleration;
            }
            
            EditorGUILayout.PropertyField(serializedObject.FindProperty("brakeForce"));
            EditorGUI.indentLevel--;
        }
        EditorGUILayout.EndFoldoutHeaderGroup();
        EditorGUILayout.Space();

        //---------------------------------------------------------------------- CENTER OF MASS POSITION --------------------------------------------------------------------------------------------\\

        showCenterOfMassPosition = EditorGUILayout.BeginFoldoutHeaderGroup(showCenterOfMassPosition, "Center Of Mass Position", foldoutStyle);
        EditorGUILayout.Space(3);

        if (showCenterOfMassPosition)
        {
            EditorGUI.indentLevel++;
            editCenterOfMassPosition = EditorGUILayout.Toggle("Edit Center Of Mass", editCenterOfMassPosition);

            if (editCenterOfMassPosition)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(serializedObject.FindProperty("centerOfMassPosition"));
                EditorGUI.indentLevel--;
            }
            else
            {
                forkliftController.centerOfMassPosition = forkliftController.defaultCenterOfMassPosition;
            }
            EditorGUI.indentLevel--;
        }
        EditorGUILayout.EndFoldoutHeaderGroup();
        EditorGUILayout.Space();

        //---------------------------------------------------------------------- FUEL SYSTEM --------------------------------------------------------------------------------------------------------\\

        showFuelSystem = EditorGUILayout.BeginFoldoutHeaderGroup(showFuelSystem, "Fuel System", foldoutStyle);
        EditorGUILayout.Space(3);

        if(showFuelSystem)
        {
            EditorGUI.indentLevel++;
            fuelSystem = EditorGUILayout.Toggle("Fuel System Activate", fuelSystem);

            if (fuelSystem)
            {
                EditorGUI.indentLevel++;
                if (forkliftController.currentFuelSlider != null)
                {
                    forkliftController.currentFuelSlider.gameObject.SetActive(true);
                }
                EditorGUILayout.PropertyField(serializedObject.FindProperty("currentFuelSlider"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("currentFuel"));
                forkliftController.currentFuelSlider.value = EditorGUILayout.Slider(forkliftController.currentFuel / forkliftController.maxFuelSlider, 0, 1);
                if (GUILayout.Button("Full Fuel", GUILayout.MaxWidth(150)))
                {   
                    forkliftController.currentFuel = 100f;
                }
                EditorGUI.indentLevel--;
            }
            else
            {
                if (forkliftController.currentFuelSlider != null)
                {
                    forkliftController.currentFuelSlider.gameObject.SetActive(false);
                }
            }

            EditorGUI.indentLevel--;
        }
        EditorGUILayout.EndFoldoutHeaderGroup();


        EditorGUILayout.Space();

        //---------------------------------------------------------------------- GEAR SYSTEM --------------------------------------------------------------------------------------------------------\\

        showGearSystem = EditorGUILayout.BeginFoldoutHeaderGroup(showGearSystem, "Gear System", foldoutStyle);
        EditorGUILayout.Space(3);

        if (showGearSystem)
        {
            EditorGUI.indentLevel++;
            gearSystem = EditorGUILayout.Toggle("Gear System Activate", gearSystem);

            if (gearSystem)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(serializedObject.FindProperty("gearUI"));
                if (forkliftController.gearUI != null)
                {
                    forkliftController.gearUI.SetActive(true);
                }

                EditorGUILayout.PropertyField(serializedObject.FindProperty("gearUI_D"));
                if (forkliftController.gearUI_D != null)
                {
                    forkliftController.gearUI_D.SetActive(true);
                }

                EditorGUILayout.PropertyField(serializedObject.FindProperty("gearUI_R"));
                if (forkliftController.gearUI_R != null)
                {
                    forkliftController.gearUI_R.SetActive(true);
                }
                EditorGUI.indentLevel--;
            }
            else
            {
                if (forkliftController.gearUI != null)
                {
                    forkliftController.gearUI.SetActive(false);
                }

                if (forkliftController.gearUI_D != null)
                {
                    forkliftController.gearUI_D.SetActive(false);
                }

                if (forkliftController.gearUI_R != null)
                {
                    forkliftController.gearUI_R.SetActive(false);
                }
            }
            EditorGUI.indentLevel--;
        }
        EditorGUILayout.EndFoldoutHeaderGroup();
        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(serializedObject.FindProperty("inGameMenu"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("steeringWheelUI"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("lift"));

        serializedObject.ApplyModifiedProperties();
    }
}
