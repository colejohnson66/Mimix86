# Mimix86 Source Generator DSL

Mimix86's [source generators](../Mimix86.Generators) works by parsing files written in a custom [DSL](https://en.wikipedia.org/wiki/Domain-specific_language).

## DSL Syntax
The DSL is designed to be a concise way of writing a node tree consisting of nothing more than strings and arrays.
Each line of the input (that is not just whitespace or comments) forms a node.

* Arrays are formed by wrapping its nodes in a pair of brackets (`[` and `]`)
* Nodes are separated by whitespace
* Strings are unquoted, but must be quoted if whitespace or brackets are needed in the contents
