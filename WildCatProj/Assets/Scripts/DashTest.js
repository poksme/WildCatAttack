#pragma strict

    var dashSpeed = 12;
    var dashJump = 2;
    var dashing = 0;
     
    private var moveDirection = Vector3.zero;
    private var dashIsTrue : boolean = false;
     
    function FixedUpdate() {
       
            if (dashing > 100) { moveDirection = Vector3 (0,0,0); dashing = 0;}
           
            if (Input.GetKey (KeyCode.Space)) {
                SendMessage("DidJump", SendMessageOptions.DontRequireReceiver);
                dash();
               
            }
           
            if (Input.GetButton ("LeftDash")) {
                SendMessage("DidJump", SendMessageOptions.DontRequireReceiver);
                dash2();
               
            }
               
       
       
     
        // Move the controller
        var controller : CharacterController = GetComponent(CharacterController);
        var flags = controller.Move(moveDirection * Time.deltaTime);
        dashing ++;
       
       
       
       
    }
     
    var DashCooldown = 2;
    private var DashTime = 0;
    function dash()
    {
       if (Time.time > DashTime)
       {
          moveDirection.x = dashSpeed;
          moveDirection.y = dashJump;
          DashTime = Time.time + DashCooldown;
          dashing ++;
       }
    }
     
    function dash2()
    {
       if (Time.time > DashTime)
       {
          moveDirection.x -= dashSpeed;
          moveDirection.y = dashJump;
          DashTime = Time.time + DashCooldown;
          dashing ++;
       }
    }
     
    @script RequireComponent(CharacterController)