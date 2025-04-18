using TaskManagerBackend;
using TaskManagerBackend.Models;
using TaskManagerBackend.Services;
using static TaskManagerBackend.Enums;

var builder = WebApplication.CreateBuilder(args);


// Update the User class initialization to match the correct property name 'UserType' instead of 'Role'.
var users = new List<User>
{
   new User { Id = 1, NameAndSurname = "Alice", UserType = Enums.UserType.Programmer },
   new User { Id = 2, NameAndSurname = "Bob", UserType = Enums.UserType.DevOpsAdministrator },
   new User { Id = 3, NameAndSurname = "Charlie", UserType = Enums.UserType.Programmer },
   new User { Id = 4, NameAndSurname = "Diana", UserType = Enums.UserType.DevOpsAdministrator },
   new User { Id = 5, NameAndSurname = "Ethan", UserType = Enums.UserType.DevOpsAdministrator },
   new User { Id = 6, NameAndSurname = "Fiona", UserType = Enums.UserType.Programmer },
   new User { Id = 7, NameAndSurname = "George", UserType = Enums.UserType.DevOpsAdministrator },
   new User { Id = 8, NameAndSurname = "Hannah", UserType = Enums.UserType.Programmer },
   new User { Id = 9, NameAndSurname = "Ian", UserType = Enums.UserType.DevOpsAdministrator },
   new User { Id = 10, NameAndSurname = "Julia", UserType = Enums.UserType.Programmer }
};

// Fix for CS1026, CS0103: Correcting syntax and ensuring properties exist in the MaintenanceTask initialization.

var tasks = new List<TaskBase>
{
    // ─── Assigned once per user (10 tasks) ───────────────────────────────────
    new MaintenanceTask
    {
        Id = 141,
        ShortDescription = "Maintenance for authentication servers",
        Difficulty = 4,
        TaskType = TaskType.Maintenance,
        Deadline = DateTime.Parse("2025-10-01"),
        ListOfServices = "Token cleanup and certificate rotation.",
        ListOfServers = "Auth-Server-1, Auth-Server-2",
        AssignedUserId = 1
    },
    new MaintenanceTask
    {
        Id = 142,
        ShortDescription = "Maintenance for CDN edge nodes",
        Difficulty = 3,
        TaskType = TaskType.Maintenance,
        Deadline = DateTime.Parse("2025-10-05"),
        ListOfServices = "Cache purge and SSL renewal.",
        ListOfServers = "CDN-Edge-1, CDN-Edge-2",
        AssignedUserId = 2
    },
    new MaintenanceTask
    {
        Id = 143,
        ShortDescription = "Maintenance for messaging brokers",
        Difficulty = 5,
        TaskType = TaskType.Maintenance,
        Deadline = DateTime.Parse("2025-10-09"),
        ListOfServices = "Cluster rebalance and queue cleanup.",
        ListOfServers = "Broker-1, Broker-2, Broker-3",
        AssignedUserId = 3
    },
    new MaintenanceTask
    {
        Id = 144,
        ShortDescription = "Maintenance for search cluster",
        Difficulty = 4,
        TaskType = TaskType.Maintenance,
        Deadline = DateTime.Parse("2025-10-13"),
        ListOfServices = "Index optimization and shard reallocation.",
        ListOfServers = "Search-Node-1, Search-Node-2",
        AssignedUserId = 4
    },
    new MaintenanceTask
    {
        Id = 145,
        ShortDescription = "Maintenance for payment gateway",
        Difficulty = 5,
        TaskType = TaskType.Maintenance,
        Deadline = DateTime.Parse("2025-10-17"),
        ListOfServices = "PCI‑DSS patching and log review.",
        ListOfServers = "Payment-GW-1",
        AssignedUserId = 5
    },
    new MaintenanceTask
    {
        Id = 146,
        ShortDescription = "Maintenance for analytics pipeline",
        Difficulty = 3,
        TaskType = TaskType.Maintenance,
        Deadline = DateTime.Parse("2025-10-21"),
        ListOfServices = "ETL job tuning and disk cleanup.",
        ListOfServers = "Analytics-Node-1, Analytics-Node-2",
        AssignedUserId = 6
    },
    new MaintenanceTask
    {
        Id = 147,
        ShortDescription = "Maintenance for object‑storage gateway",
        Difficulty = 4,
        TaskType = TaskType.Maintenance,
        Deadline = DateTime.Parse("2025-10-25"),
        ListOfServices = "Bucket consistency check and firmware update.",
        ListOfServers = "Storage-GW-1",
        AssignedUserId = 7
    },
    new MaintenanceTask
    {
        Id = 148,
        ShortDescription = "Maintenance for Kubernetes control‑plane",
        Difficulty = 5,
        TaskType = TaskType.Maintenance,
        Deadline = DateTime.Parse("2025-10-29"),
        ListOfServices = "k8s version upgrade and etcd defrag.",
        ListOfServers = "K8s-Master-1, K8s-Master-2",
        AssignedUserId = 8
    },
    new MaintenanceTask
    {
        Id = 149,
        ShortDescription = "Maintenance for SMTP relays",
        Difficulty = 2,
        TaskType = TaskType.Maintenance,
        Deadline = DateTime.Parse("2025-11-02"),
        ListOfServices = "RBL review and queue flush.",
        ListOfServers = "SMTP-Relay-1, SMTP-Relay-2",
        AssignedUserId = 9
    },
    new MaintenanceTask
    {
        Id = 150,
        ShortDescription = "Maintenance for VPN concentrators",
        Difficulty = 3,
        TaskType = TaskType.Maintenance,
        Deadline = DateTime.Parse("2025-11-06"),
        ListOfServices = "Firmware upgrade and user‑cert rotation.",
        ListOfServers = "VPN-Conc-1",
        AssignedUserId = 10
    },

    // ─── Unassigned pool (20 tasks) ──────────────────────────────────────────
    new MaintenanceTask
    {
        Id = 151,
        ShortDescription = "Maintenance for IoT gateway",
        Difficulty = 2,
        TaskType = TaskType.Maintenance,
        Deadline = DateTime.Parse("2025-11-10"),
        ListOfServices = "Protocol patching and device audit.",
        ListOfServers = "IoT-GW-1",
        AssignedUserId = null
    },
    new MaintenanceTask
    {
        Id = 152,
        ShortDescription = "Maintenance for load balancers",
        Difficulty = 4,
        TaskType = TaskType.Maintenance,
        Deadline = DateTime.Parse("2025-11-14"),
        ListOfServices = "Firmware flash and config backup.",
        ListOfServers = "LB-1, LB-2",
        AssignedUserId = null
    },
    new MaintenanceTask
    {
        Id = 153,
        ShortDescription = "Maintenance for Redis cache cluster",
        Difficulty = 3,
        TaskType = TaskType.Maintenance,
        Deadline = DateTime.Parse("2025-11-18"),
        ListOfServices = "Memory purge and backup verification.",
        ListOfServers = "Redis-1, Redis-2",
        AssignedUserId = null
    },
    new MaintenanceTask
    {
        Id = 154,
        ShortDescription = "Maintenance for Git hosting platform",
        Difficulty = 4,
        TaskType = TaskType.Maintenance,
        Deadline = DateTime.Parse("2025-11-22"),
        ListOfServices = "Upgrade runner agents and prune hooks.",
        ListOfServers = "Git-Host-1",
        AssignedUserId = null
    },
    new MaintenanceTask
    {
        Id = 155,
        ShortDescription = "Maintenance for static‑content servers",
        Difficulty = 2,
        TaskType = TaskType.Maintenance,
        Deadline = DateTime.Parse("2025-11-26"),
        ListOfServices = "Content sync and header hardening.",
        ListOfServers = "Static-1, Static-2",
        AssignedUserId = null
    },
    new MaintenanceTask
    {
        Id = 156,
        ShortDescription = "Maintenance for SFTP gateways",
        Difficulty = 2,
        TaskType = TaskType.Maintenance,
        Deadline = DateTime.Parse("2025-11-30"),
        ListOfServices = "Key rotation and chroot validation.",
        ListOfServers = "SFTP-GW-1",
        AssignedUserId = null
    },
    new MaintenanceTask
    {
        Id = 157,
        ShortDescription = "Maintenance for API rate‑limit service",
        Difficulty = 3,
        TaskType = TaskType.Maintenance,
        Deadline = DateTime.Parse("2025-12-04"),
        ListOfServices = "Quota reset and rule tuning.",
        ListOfServers = "RateLimit-1",
        AssignedUserId = null
    },
    new MaintenanceTask
    {
        Id = 158,
        ShortDescription = "Maintenance for monitoring stack",
        Difficulty = 4,
        TaskType = TaskType.Maintenance,
        Deadline = DateTime.Parse("2025-12-08"),
        ListOfServices = "Exporter updates and retention adjust.",
        ListOfServers = "Prometheus-1, Grafana-1",
        AssignedUserId = null
    },
    new MaintenanceTask
    {
        Id = 159,
        ShortDescription = "Maintenance for SIEM platform",
        Difficulty = 5,
        TaskType = TaskType.Maintenance,
        Deadline = DateTime.Parse("2025-12-12"),
        ListOfServices = "Rule pack upgrade and storage cleanup.",
        ListOfServers = "SIEM-1",
        AssignedUserId = null
    },
    new MaintenanceTask
    {
        Id = 160,
        ShortDescription = "Maintenance for license server",
        Difficulty = 2,
        TaskType = TaskType.Maintenance,
        Deadline = DateTime.Parse("2025-12-16"),
        ListOfServices = "License refresh and service restart.",
        ListOfServers = "License-Server-1",
        AssignedUserId = null
    },
    new MaintenanceTask
    {
        Id = 161,
        ShortDescription = "Maintenance for telemetry agents",
        Difficulty = 3,
        TaskType = TaskType.Maintenance,
        Deadline = DateTime.Parse("2025-12-20"),
        ListOfServices = "Agent rollout and metric validation.",
        ListOfServers = "Telemetry-1, Telemetry-2",
        AssignedUserId = null
    },
    new MaintenanceTask
    {
        Id = 162,
        ShortDescription = "Maintenance for time‑sync services",
        Difficulty = 2,
        TaskType = TaskType.Maintenance,
        Deadline = DateTime.Parse("2025-12-24"),
        ListOfServices = "NTP drift check and peer update.",
        ListOfServers = "NTP-1",
        AssignedUserId = null
    },
    new MaintenanceTask
    {
        Id = 163,
        ShortDescription = "Maintenance for backup repository",
        Difficulty = 3,
        TaskType = TaskType.Maintenance,
        Deadline = DateTime.Parse("2025-12-28"),
        ListOfServices = "Prune old snapshots and capacity check.",
        ListOfServers = "Backup-Repo-1",
        AssignedUserId = null
    },
    new MaintenanceTask
    {
        Id = 164,
        ShortDescription = "Maintenance for firewall clusters",
        Difficulty = 4,
        TaskType = TaskType.Maintenance,
        Deadline = DateTime.Parse("2026-01-01"),
        ListOfServices = "Policy audit and firmware upgrade.",
        ListOfServers = "FW-Cluster-1, FW-Cluster-2",
        AssignedUserId = null
    },
    new MaintenanceTask
    {
        Id = 165,
        ShortDescription = "Maintenance for reporting engine",
        Difficulty = 2,
        TaskType = TaskType.Maintenance,
        Deadline = DateTime.Parse("2026-01-05"),
        ListOfServices = "Report cache purge and scheduler update.",
        ListOfServers = "Reporting-1",
        AssignedUserId = null
    },
    new MaintenanceTask
    {
        Id = 166,
        ShortDescription = "Maintenance for container registry",
        Difficulty = 3,
        TaskType = TaskType.Maintenance,
        Deadline = DateTime.Parse("2026-01-09"),
        ListOfServices = "Garbage collect layers and upgrade TLS.",
        ListOfServers = "Registry-1",
        AssignedUserId = null
    },
    new MaintenanceTask
    {
        Id = 167,
        ShortDescription = "Maintenance for AI inference nodes",
        Difficulty = 5,
        TaskType = TaskType.Maintenance,
        Deadline = DateTime.Parse("2026-01-13"),
        ListOfServices = "Driver update and model refresh.",
        ListOfServers = "AI-Node-1, AI-Node-2",
        AssignedUserId = null
    },
    new MaintenanceTask
    {
        Id = 168,
        ShortDescription = "Maintenance for WebSocket gateway",
        Difficulty = 3,
        TaskType = TaskType.Maintenance,
        Deadline = DateTime.Parse("2026-01-17"),
        ListOfServices = "Connection limit test and cert renewal.",
        ListOfServers = "WS-GW-1",
        AssignedUserId = null
    },
    new MaintenanceTask
    {
        Id = 169,
        ShortDescription = "Maintenance for data‑warehouse ETL",
        Difficulty = 4,
        TaskType = TaskType.Maintenance,
        Deadline = DateTime.Parse("2026-01-21"),
        ListOfServices = "Partition maintenance and job reschedule.",
        ListOfServers = "DWH-ETL-1",
        AssignedUserId = null
    },
    new MaintenanceTask
    {
        Id = 170,
        ShortDescription = "Maintenance for SaaS integration hooks",
        Difficulty = 3,
        TaskType = TaskType.Maintenance,
        Deadline = DateTime.Parse("2026-01-25"),
        ListOfServices = "Webhook rotation and retry‑queue purge.",
        ListOfServers = "Integration-1",
        AssignedUserId = null
    }
};


// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ITaskServices, TaskService>(provider =>
{
    return new TaskService(tasks, users);
});
builder.Services.AddSingleton<List<User>>(users);
builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularDev", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseCors("AllowAngularDev");
app.MapControllers();
app.Run();
