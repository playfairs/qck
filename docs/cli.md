# QCK CLI Reference

## Overview

QCK provides a command-line interface for quantum cryptography operations. All commands follow the pattern `qck <command> [options]`.

## Commands

### random

Generate quantum random bits.

#### Usage

```bash
qck random [bits]
```

#### Arguments

- `bits` (optional): Number of random bits to generate. Default: 32.

#### Examples

```bash
qck random
qck random 32
qck random 128
```

#### Output

Outputs a string of 0s and 1s representing the quantum random bits.

#### Exit Codes

- 0: Success
- 1: Error (invalid bit count)

---

### exchange

Run a BB84 quantum key exchange.

#### Usage

```bash
qck exchange [bits]
```

#### Arguments

- `bits` (optional): Number of bits to exchange. Default: 256.

#### Examples

```bash
qck exchange
qck exchange 256
qck exchange 512
```

#### Output

Displays:
- Total bits exchanged
- Alice's bases (+ or ×)
- Alice's bits (0 or 1)
- Bob's bases (+ or ×)
- Bob's bits (0 or 1)
- Shared key (matching bits)
- Shared key length

If the shared key is at least 32 bits, it is automatically saved to `.qck_key`.

#### Exit Codes

- 0: Success
- 1: Error (invalid bit count)

---

### encrypt

Encrypt a file using AES-256-GCM with the shared key.

#### Usage

```bash
qck encrypt <file>
```

#### Arguments

- `file` (required): Path to the file to encrypt.

#### Examples

```bash
qck encrypt document.txt
qck encrypt /path/to/file.dat
```

#### Behavior

- If `.qck_key` exists, uses the existing key.
- If `.qck_key` does not exist, generates a new key and saves it.
- Creates an encrypted file with `.enc` extension.

#### Output

- Confirmation of key usage or generation
- Path to encrypted file

#### Exit Codes

- 0: Success
- 1: Error (file not found, key error, encryption error)

---

### decrypt

Decrypt a file encrypted with `qck encrypt`.

#### Usage

```bash
qck decrypt <file>
```

#### Arguments

- `file` (required): Path to the encrypted file.

#### Examples

```bash
qck decrypt document.txt.enc
qck decrypt /path/to/file.dat.enc
```

#### Behavior

- Requires `.qck_key` to exist in the current directory.
- Removes `.enc` extension from output file name.
- Uses AES-256-GCM for decryption.

#### Output

- Path to decrypted file

#### Exit Codes

- 0: Success
- 1: Error (file not found, key not found, decryption error)

---

### explain

Display an explanation of the BB84 protocol and QCK implementation.

#### Usage

```bash
qck explain
```

#### Arguments

None.

#### Output

Displays detailed information about:
- BB84 protocol overview
- Protocol steps
- Quantum properties used
- Security considerations
- QCK implementation details
- Usage examples

#### Exit Codes

- 0: Success

---

## Global Options

None. All options are command-specific.

## Exit Codes

All commands return:
- `0`: Success
- `1`: Error

Error messages are printed to stderr.

## File Operations

### Key File

- Location: `.qck_key` in current directory
- Format: Binary (32 bytes for AES-256)
- Created by: `qck exchange` (if key ≥ 32 bits) or `qck encrypt` (if no key exists)
- Used by: `qck encrypt` and `qck decrypt`

### Encrypted Files

- Extension: `.enc`
- Format: [nonce (12 bytes)][tag (16 bytes)][ciphertext]
- Created by: `qck encrypt`
- Decrypted by: `qck decrypt`

## Error Handling

### Common Errors

- **File not found**: The specified file does not exist.
- **Invalid bit count**: The bit count must be a positive integer.
- **Key not found**: No `.qck_key` file exists for decryption.
- **Key error**: Failed to load or generate a key.
- **Encryption/Decryption error**: Cryptographic operation failed.

### Error Messages

All errors print a descriptive message to stderr and exit with code 1.

## Exampls

### Complete Workflow

```bash
# generate a shared key via BB84
qck exchange 512

qck encrypt secret.txt

qck decrypt secret.txt.enc

qck random 64

qck explain
```

### Quick Start

```bash
qck encrypt myfile.txt

qck decrypt myfile.txt.enc
```

## Notes

- All quantum operations run in a simulator, not on real quantum hardware.
- The system is for educational purposes only.
- Not suitable for production security use.
- Keys are stored unencrypted in the current directory.
