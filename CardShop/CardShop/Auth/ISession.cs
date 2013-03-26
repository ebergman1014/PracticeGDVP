using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardShop.Auth
{
    public interface ISession
    {
        // Summary:
        //     Gets or sets a session value by numerical index.
        //
        // Parameters:
        //   index:
        //     The numerical index of the session value.
        //
        // Returns:
        //     The session-state value stored at the specified index, or null if the item
        //     does not exist.
        object this[int index] { get; set; }
        //
        // Summary:
        //     Gets or sets a session value by name.
        //
        // Parameters:
        //   name:
        //     The key name of the session value.
        //
        // Returns:
        //     The session-state value with the specified name, or null if the item does
        //     not exist.
        object this[string name] { get; set; }
    }
}
