 var thePrefab : GameObject; 
 var count : int = 10; // Number of cubes
 var sphereSize : float = 10; // Radius of sphere
 
 function Start () {
     var center = transform.position;
     for (var i=0; i < count; ++i) {
         //yield WaitForSeconds (0.5); // Spawning delay
        var pos = Random.onUnitSphere * sphereSize;
         var rot = Quaternion.FromToRotation(Vector3.forward, center-pos); // Face cubes to center
         Instantiate(thePrefab, pos, rot);
     }
 }