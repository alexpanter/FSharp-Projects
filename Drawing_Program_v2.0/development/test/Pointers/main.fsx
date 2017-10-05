#nowarn "9"

open Microsoft.FSharp.NativeInterop

let a = 5

let ptr_a = NativePtr.read a

//let ptr_a = NativeInterop.NativePtr.add<int>(nativeptr<int>)


