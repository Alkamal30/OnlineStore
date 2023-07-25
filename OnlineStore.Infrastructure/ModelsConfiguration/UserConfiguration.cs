using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Common.Enums;
using OnlineStore.Infrastructure.Models;

namespace OnlineStore.Infrastructure.ModelsConfiguration;

public class UserConfiguration : IEntityTypeConfiguration<User> {
	public void Configure(EntityTypeBuilder<User> builder) {
		builder.HasData(
			new User {
				Id = 1,
				Login = "Admin",
				Password = "Admin",
				Role = UserRole.Admin
			},
			new User {
				Id = 2,
				Login = "User",
				Password = "User",
				Role = UserRole.User
			}
		);
	}}
