﻿using System;
using System.Collections.Generic;
using System.Text;

namespace GraphAlgorithms
{
    public class VertexIndexOutOfRangeException : Exception { public VertexIndexOutOfRangeException(string message) : base(message) { } }
    public class NotConnectedGraphException : Exception { public NotConnectedGraphException(string message) : base(message) { } }
    public class SelfLoopException : Exception { public SelfLoopException(string message) : base(message) { } }
    public class ParallelEdgeException : Exception { public ParallelEdgeException(string message) : base(message) { } }
    public class ParallelEdgesOrSelfLoopsAllowedException : Exception { public ParallelEdgesOrSelfLoopsAllowedException(string message) : base(message) { } };
    public class InconsistentEdgeException : Exception { public InconsistentEdgeException(string message) : base(message) { } };
}
