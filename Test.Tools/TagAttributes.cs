// <copyright file="TagAttributes.cs" company="EmergeTech LLC">
// Copyright (c) EmergeTech LLC. All rights reserved.
// Reproduction or transmission in whole or in part, in any form or
// by any means, electronic, mechanical or otherwise, is prohibited
// without the prior written consent of the copyright owner.
// </copyright>

using System.ComponentModel;

namespace Test.Tools
{
  public enum TagAttributes
  {
    [Description("id")] Id,

    [Description("name")] Name,

    [Description("class")] Class,

    [Description("value")] Value
  }
}