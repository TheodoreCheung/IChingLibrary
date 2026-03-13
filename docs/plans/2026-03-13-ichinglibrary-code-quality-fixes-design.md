# IChingLibrary Code Quality Fixes Design

Date: 2026-03-13

## Context
Recent review found internal inconsistencies and minor inefficiencies in SixLines and Core:
- Time-based hexagram generation bypasses custom inquiry time providers.
- Random inputs allow negative values leading to invalid modulo behavior.
- HiddenDeity step can run before SixKin, causing runtime exceptions.
- Small repeated allocations in line position lookup and empty branches lookup.
- DefaultSymbolicStarProvider.Add comment does not match behavior.

## Goals
- Keep all public API signatures and behaviors unchanged.
- Ensure time conversion is performed once and consistently used for hexagram generation.
- Validate random inputs to avoid undefined modulo outcomes.
- Guard step dependencies with clear exceptions.
- Reduce unnecessary allocations in hot paths.
- Align documentation with actual behavior.

## Non-Goals
- No public API additions or signature changes.
- No behavioral changes to domain rules or mappings.
- No changes to external dependencies.

## Approach (Recommended: A)
1. Builder state for hexagram source
   - Use internal state to record source and parameters in Use* methods.
   - Build() resolves inquiry time once, then generates four symbols.
2. HexagramGenerator overloads
   - Add internal overloads that accept already-converted InquiryTime data.
   - Use these from Build() to avoid repeated conversions.
3. Input validation
   - In UseRandomHexagram or generator, validate inputs and throw ArgumentOutOfRangeException.
4. Step dependency guard
   - DefaultHiddenDeityProvider validates SixKin is bound; otherwise throw InvalidOperationException.
5. Micro-optimizations
   - Cache line positions (or use LinePosition.FromArrayIndex).
   - Use direct lookup for EmptyBranches without repeated GetAll scans.
6. Documentation alignment
   - Update DefaultSymbolicStarProvider.Add comment to reflect TryAdd behavior (no overwrite).

## Data Flow
Use* -> record source -> Build():
- Resolve InquiryTime once (provider or default).
- Generate FourSymbols using resolved data.
- Bind steps in configured order.

## Error Handling
- Invalid random inputs -> ArgumentOutOfRangeException with parameter name.
- Missing dependency (SixKin before HiddenDeity) -> InvalidOperationException with guidance.

## Testing
- Update or add unit tests to cover:
  - Time-based generation uses the same inquiry time provider.
  - Negative/random inputs throw expected exceptions.
  - HiddenDeity without SixKin throws clear error.
  - No changes to existing expected outputs.

## Risks
- Internal refactor could subtly alter generation order; mitigate with targeted tests.
