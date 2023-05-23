using System;
namespace Web.Models
{
	public class TeamSocial
	{
		public int Id { get; set; }
		public int TeamId { get; set; }
		public Team Team { get; set; }
		public int SocialId { get; set; }
		public Social Social { get; set; }
		public string UserUrl { get; set; }
	}
}

