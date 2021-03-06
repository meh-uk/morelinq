#region License and Terms
// MoreLINQ - Extensions to LINQ to Objects
// Copyright (c) 2008 Jonathan Skeet. All rights reserved.
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
#endregion

using System;
using NUnit.Framework;

namespace MoreLinq.Test
{
    [TestFixture]
    public class TagFirstLastTest
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TagFirstLastNullSource()
        {
            MoreEnumerable.TagFirstLast<object, object>(null, delegate { return null; });
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TagFirstLastNullResultSelector()
        {
            new object[0].TagFirstLast<object, object>(null);
        }

        [Test]
        public void TagFirstLastIsLazy()
        {
            new BreakingSequence<object>().TagFirstLast(delegate { return 0; });
        }

        [Test]
        public void TagFirstLastWideSourceSequenceOfOne()
        {
            var source = new[] { 123 };
            source.TagFirstLast((item, isFirst, isLast) => new { Item = item, IsFirst = isFirst, IsLast = isLast })
                  .AssertSequenceEqual(new { Item = 123, IsFirst = true, IsLast = true });
        }

        [Test]
        public void TagFirstLastWideSourceSequenceOfTwo()
        {
            var source = new[] { 123, 456 };
            source.TagFirstLast((item, isFirst, isLast) => new { Item = item, IsFirst = isFirst, IsLast = isLast })
                  .AssertSequenceEqual(new { Item = 123, IsFirst = true,  IsLast = false },
                                       new { Item = 456, IsFirst = false, IsLast = true });
        }

        [Test]
        public void TagFirstLastWideSourceSequenceOfThree()
        {
            var source = new[] { 123, 456, 789 };
            source.TagFirstLast((item, isFirst, isLast) => new { Item = item, IsFirst = isFirst, IsLast = isLast })
                  .AssertSequenceEqual(new { Item = 123, IsFirst = true,  IsLast = false },
                                       new { Item = 456, IsFirst = false, IsLast = false },
                                       new { Item = 789, IsFirst = false, IsLast = true  });
        }
    }
}
