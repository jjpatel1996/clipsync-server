﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRHubConsole {
	static class Users {

		private static Dictionary<string, ArrayList> users_dictionary = new Dictionary<string, ArrayList>();


		public static void AddUserConnection(string uid, UserConnection userConnection) {

			if (users_dictionary.ContainsKey(uid)) {
				//if the user has already few connected devices and one more device is connecting now so adding to existing list
				ArrayList current_list = users_dictionary[uid];
				current_list.Add(userConnection);
				users_dictionary[uid] = current_list;
				Console.WriteLine("Updated user uid "+uid+" connection list");
				//Currently every new connection even from the same device id is added in future if the device id is same but differen connectionID then need to handle that case
			} else {
				// If the new connection is first connection of the user
				users_dictionary.Add(uid, new ArrayList());
				ArrayList current_list = users_dictionary[uid];
				current_list.Add(userConnection);
				users_dictionary[uid] = current_list;
				Console.WriteLine("New user uid " + uid + " connection list created"); 
			}

			ArrayList connections = users_dictionary[uid];
			Console.WriteLine("Total Number of Connections now are : " + connections.Count);
		}

		public static ArrayList GetUserConnections(string uid) {
			if (users_dictionary.ContainsKey(uid)) {
				return users_dictionary[uid];
			} else {
				return null;
			}
		}

		public static void DeleteUserConnection(string uid, string connection_id) {
			if (users_dictionary.ContainsKey(uid)) {

				int index_to_delete = -1;

				ArrayList current_list = users_dictionary[uid];
				for (int i = 0; i < current_list.Count; i++) {
					UserConnection userConnection = (UserConnection)current_list[i];
					if (userConnection.Getconnection_id().Equals(connection_id)) {
						index_to_delete = i;
						break;
					} else {
						// Do Nothing as this is not the connection to delete
					}

				}

				// Now deleting at the given index
				if (index_to_delete != -1) {
					UserConnection userConnection = (UserConnection)current_list[index_to_delete];
					Console.WriteLine("Deleted Successfully a connection of user : " + uid + " connection id : " +userConnection.Getconnection_id() +" Platform : " + userConnection.Getplatform() + " Device Id : " +userConnection.Getdevice_id());
					current_list.RemoveAt(index_to_delete);
					users_dictionary[uid] = current_list;
				}

			}
		}

		public static string GetUIDFromConnectionID(string connection_id) {
			string[] uids = users_dictionary.Keys.ToArray();

			foreach (string uid in uids) {
				ArrayList current_list = users_dictionary[uid];
				for (int i = 0; i < current_list.Count; i++) {
					UserConnection userConnection = (UserConnection)current_list[i];
					if (userConnection.Getconnection_id().Equals(connection_id)) {
						return uid;
					}
				}
			}
			return "";
		}

	}
}
