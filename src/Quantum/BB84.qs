namespace QCK.Quantum;

open Microsoft.Quantum.Intrinsic;
open Microsoft.Quantum.Measurement;
open Microsoft.Quantum.Convert;
open Microsoft.Quantum.Arrays;
open Microsoft.Quantum.Math;

operation PrepareQubit(bit : Result, basis : Int) : Unit {
    use q = Qubit();
    if bit == One {
        X(q);
    }
    if basis == 1 {
        H(q);
    }
}

operation MeasureQubit(basis : Int) : Result {
    use q = Qubit();
    if basis == 1 {
        H(q);
    }
    let result = M(q);
    Reset(q);
    return result;
}

operation GenerateSharedKey(bitCount : Int) : (Result[], Int[], Int[], Result[], Int[]) {
    mutable aliceBits = new Result[bitCount];
    mutable aliceBases = new Int[bitCount];
    mutable bobBases = new Int[bitCount];
    mutable bobBits = new Result[bitCount];
    
    for i in 0..bitCount - 1 {
        set aliceBits w/ i <- RandomBit();
        set aliceBases w/ i <- RandomInt(2);
        set bobBases w/ i <- RandomInt(2);
        
        use q = Qubit();
        if aliceBits[i] == One {
            X(q);
        }
        if aliceBases[i] == 1 {
            H(q);
        }
        
        if bobBases[i] == 1 {
            H(q);
        }
        
        set bobBits w/ i <- M(q);
        Reset(q);
    }
    
    mutable sharedKey = new Result[0];
    for i in 0..bitCount - 1 {
        if aliceBases[i] == bobBases[i] {
            set sharedKey += [aliceBits[i]];
        }
    }
    
    return (aliceBits, aliceBases, bobBases, bobBits, sharedKey);
}
