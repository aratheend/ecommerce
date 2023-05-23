using System;
namespace Web.Models
{
	public class Team
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Surname { get; set; }
		public string PhotoUrl { get; set; }
		public int PositionId { get; set; }
		public Position Position { get; set; }
		public List<TeamSocial> TeamSocials { get; set; }
	}
}

