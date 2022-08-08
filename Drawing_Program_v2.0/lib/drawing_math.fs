module math


type Vector() = class
    
    // Correcting Angle
    // if (angle < 0) or (angle > 2 * PI)
    static member AngleFix(a: float) =
        if a < 0.0 then
            a + 2.0 * System.Math.PI
        elif a > 2.0 * System.Math.PI then
            a - 2.0 * System.Math.PI
        else a


    // Get the vector norm
    static member VectorNorm(x: float, y: float) =
        x * x + y * y |> System.Math.Sqrt


    // Rotate vector
    static member RotateVector(x: float, y: float)(velocity: float) =
        let norm = Vector.VectorNorm(x,y)
        let v = System.Math.Asin(1.0 / norm * (float y)) + velocity |> Vector.AngleFix
        (norm * System.Math.Cos v, norm * System.Math.Sin v)


    // change the vector norm
    static member ScaleVector(x: float, y: float)(scalar: float) =
        (x * scalar, y * scalar)


    // move the vector
    static member MoveVector(x: float, y: float)(moveX: float, moveY: float) =
        (x + moveX, y + moveY)

end
