namespace QCK.Quantum;

open Microsoft.Quantum.Intrinsic;
open Microsoft.Quantum.Measurement;
open Microsoft.Quantum.Canon;
open Microsoft.Quantum.Convert;
open Microsoft.Quantum.Arrays;

operation RandomBit() : Result {
    use q = Qubit();
    H(q);
    let result = M(q);
    Reset(q);
    return result;
}

operation RandomBits(count : Int) : Result[] {
    mutable results = new Result[count];
    for i in 0..count - 1 {
        set results w/ i <- RandomBit();
    }
    return results;
}