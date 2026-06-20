namespace QCK.Quantum;

open Microsoft.Quantum.Convert;
open Microsoft.Quantum.Arrays;

function ResultArrayToInt(results : Result[]) : Int {
    mutable value = 0;
    for i in 0..Length(results) - 1 {
        if results[i] == One {
            set value += 1 <<< i;
        }
    }
    return value;
}

function ResultArrayToBoolArray(results : Result[]) : Bool[] {
    return Mapped(ResultToBool, results);
}

function BoolArrayToResultArray(booleans : Bool[]) : Result[] {
    return Mapped(BoolToResult, booleans);
}

function ResultArrayToByte(results : Result[]) : Byte {
    return ResultArrayToInt(results) |> AsByte;
}
