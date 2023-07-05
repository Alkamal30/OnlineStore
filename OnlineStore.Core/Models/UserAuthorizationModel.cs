﻿using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Core.Models;

public class UserAuthorizationModel {

	public string Login { get; set; } = "";
	public string Password { get; set; } = "";
}