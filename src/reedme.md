# Architecture

## Domain

- Contains pure business logic.
- Doesn't depend on any external dependency.
- You should be able to write all the unit tests without any mocks.
- Ussually doesn't define interfaces.

## Application

### Use Cases

- Defined and implemented in `Application.Ports.In`.

### Services

- Services that can be used by several use cases.

### Out Ports

- Interface definitions, like a database repository, or external APIs.
- The interfaces are ussually implemented in the `Adapter.Out`.
