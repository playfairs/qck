# QCK Architecture

## Overview

QCK follows clean architecture principles with clear separation between quantum and classical layers. The system is designed to be modular, maintainable, and production-quality.

## Layer Architecture

### Quantum Layer

The quantum layer is implemented in Q# and handles all quantum operations:

- **Random.qs**: Quantum randomness generation using superposition
- **BB84.qs**: BB84 protocol implementation for quantum key distribution
- **Utilities.qs**: Helper functions for quantum-to-classical conversions

#### Quantum Operations

- `RandomBit()`: Generate a single quantum random bit
- `RandomBits(count)`: Generate multiple quantum random bits
- `PrepareQubit(bit, basis)`: Prepare a qubit in a specific state
- `MeasureQubit(basis)`: Measure a qubit in a specific basis
- `GenerateSharedKey(bitCount)`: Execute full BB84 protocol

### Classical Layer

The classical layer is implemented in C# and handles all application logic:

#### Program Entry Point

- **Program.cs**: Argument parsing and command dispatching

#### Commands

- **RandomCommand.cs**: Execute quantum randomness operations
- **ExchangeCommand.cs**: Execute BB84 exchanges and display results
- **EncryptCommand.cs**: File encryption using AES-256-GCM
- **DecryptCommand.cs**: File decryption using AES-256-GCM
- **ExplainCommand.cs**: Educational protocol explanation

#### Cryptography

- **Models.cs**: Data models (SharedKey, ExchangeResult, EncryptionResult)
- **KeyManager.cs**: Key generation, storage, and loading
- **AesEncryption.cs**: AES-256-GCM encryption and decryption

## Data Flow

### Random Number Generation

```
User CLI
  ↓
RandomCommand
  ↓
QuantumSimulator
  ↓
RandomBits (Q#)
  ↓
Hadamard Gate + Measurement
  ↓
Random Bits Output
```

### BB84 Key Exchange

```
User CLI
  ↓
ExchangeCommand
  ↓
QuantumSimulator
  ↓
GenerateSharedKey (Q#)
  ↓
Alice: Generate random bits and bases
  ↓
Alice: Encode qubits
  ↓
Bob: Choose random bases
  ↓
Bob: Measure qubits
  ↓
Basis comparison
  ↓
Shared key extraction
  ↓
Key storage (.qck_key)
```

### File Encryption

```
User CLI
  ↓
EncryptCommand
  ↓
KeyManager (load or generate key)
  ↓
AesEncryption
  ↓
AES-256-GCM encryption
  ↓
Encrypted file output
```

### File Decryption

```
User CLI
  ↓
DecryptCommand
  ↓
KeyManager (load key)
  ↓
AesEncryption
  ↓
AES-256-GCM decryption
  ↓
Decrypted file output
```

## Security Considerations

### Quantum Layer

- Quantum randomness is truly random (not pseudo-random)
- BB84 protocol provides theoretical security against eavesdropping
- Measurement in wrong basis produces random results
- No-cloning theorem prevents quantum state copying

### Classical Layer

- AES-256-GCM provides authenticated encryption
- Random nonce for each encryption operation
- Authentication tag ensures integrity
- Secure key handling using System.Security.Cryptography

### Limitations

- Quantum operations run in simulator (not real quantum hardware)
- Key exchange is simulated (single machine, not network)
- Not suitable for production security use
- Educational and demonstration purposes only

## Component Responsibilities

### Quantum Layer Responsibilities

- Generate quantum randomness
- Execute BB84 protocol steps
- Perform quantum measurements
- Handle basis selection
- Extract shared key from matched bases

### Classical Layer Responsibilities

- Parse command-line arguments
- Dispatch to appropriate command handlers
- Manage file I/O
- Handle key storage and retrieval
- Perform AES encryption/decryption
- Format and display output
- Provide educational explanations

## Error Handling

All components implement proper error handling:

- Invalid input validation
- File existence checks
- Key validation
- Cryptographic error handling
- Clear error messages with exit codes

## Testing Strategy

Unit tests cover:

- Quantum randomness generation
- BB84 key exchange logic
- Key extraction from matched bases
- Encryption and decryption operations
- Invalid input handling
- File operations
