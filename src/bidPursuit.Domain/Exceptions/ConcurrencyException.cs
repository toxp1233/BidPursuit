namespace bidPursuit.Domain.Exceptions;

public class ConcurrencyException(string message) : Exception(message);
