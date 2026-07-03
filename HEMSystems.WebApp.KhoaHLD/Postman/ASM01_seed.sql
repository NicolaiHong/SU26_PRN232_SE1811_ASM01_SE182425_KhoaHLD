SET NOCOUNT ON;

IF NOT EXISTS (
    SELECT 1
    FROM [dbo].[System.UserAccount]
    WHERE [Email] = 'admin@hem.com'
)
BEGIN
    INSERT INTO [dbo].[System.UserAccount] (
        [UserName],
        [Password],
        [FullName],
        [Email],
        [Phone],
        [EmployeeCode],
        [RoleId],
        [RequestCode],
        [CreatedDate],
        [ApplicationCode],
        [CreatedBy],
        [ModifiedDate],
        [ModifiedBy],
        [IsActive]
    )
    VALUES (
        'admin',
        '123456',
        'ASM01 Administrator',
        'admin@hem.com',
        '0900000001',
        'EMP001',
        1,
        'REQ-ASM01-ADMIN',
        GETDATE(),
        'ASM01',
        'seed',
        GETDATE(),
        'seed',
        1
    );
END;

IF NOT EXISTS (
    SELECT 1
    FROM [DevelopmentPlatformsKhoaHLD]
    WHERE [platform_name] = 'Azure App Service'
)
BEGIN
    INSERT INTO [DevelopmentPlatformsKhoaHLD] (
        [platform_name],
        [platform_description],
        [website_url],
        [created_by],
        [support_email],
        [is_active],
        [created_at],
        [updated_at]
    )
    VALUES (
        'Azure App Service',
        'Cloud platform for hosting hackathon web applications.',
        'https://azure.microsoft.com/products/app-service',
        'seed',
        'azure-support@example.com',
        1,
        GETDATE(),
        GETDATE()
    );
END;

IF NOT EXISTS (
    SELECT 1
    FROM [DevelopmentPlatformsKhoaHLD]
    WHERE [platform_name] = 'Vercel'
)
BEGIN
    INSERT INTO [DevelopmentPlatformsKhoaHLD] (
        [platform_name],
        [platform_description],
        [website_url],
        [created_by],
        [support_email],
        [is_active],
        [created_at],
        [updated_at]
    )
    VALUES (
        'Vercel',
        'Frontend deployment platform for demo projects.',
        'https://vercel.com',
        'seed',
        'vercel-support@example.com',
        1,
        GETDATE(),
        GETDATE()
    );
END;

DECLARE @AzurePlatformId int = (
    SELECT TOP 1 [platform_Khoahld_id]
    FROM [DevelopmentPlatformsKhoaHLD]
    WHERE [platform_name] = 'Azure App Service'
);

DECLARE @VercelPlatformId int = (
    SELECT TOP 1 [platform_Khoahld_id]
    FROM [DevelopmentPlatformsKhoaHLD]
    WHERE [platform_name] = 'Vercel'
);

IF NOT EXISTS (
    SELECT 1
    FROM [ProjectSubmissionsKhoaHLD]
    WHERE [project_name] = 'AI Hackathon Assistant'
)
BEGIN
    INSERT INTO [ProjectSubmissionsKhoaHLD] (
        [team_id],
        [round_id],
        [platform_Khoahld_id],
        [project_name],
        [project_description],
        [repository_url],
        [demo_url],
        [document_url],
        [repository_size_mb],
        [version_number],
        [submitted_by],
        [submission_status],
        [is_deployed],
        [is_late_submission],
        [submitted_at],
        [updated_at]
    )
    VALUES
        ('TEAM01', 'R1', @AzurePlatformId, 'AI Hackathon Assistant', 'AI assistant for hackathon management.', 'https://github.com/example/ai-hackathon-assistant', 'https://example.com/ai-demo', 'https://example.com/ai-doc', 12.50, 1, 'KhoaHLD', 'Submitted', 1, 0, '2026-07-01T09:00:00', '2026-07-01T09:00:00'),
        ('TEAM01', 'R1', @VercelPlatformId, 'Smart Registration Portal', 'Portal for team registration and validation.', 'https://github.com/example/smart-registration', 'https://example.com/registration-demo', 'https://example.com/registration-doc', 18.75, 1, 'KhoaHLD', 'Submitted', 1, 0, '2026-07-01T09:05:00', '2026-07-01T09:05:00'),
        ('TEAM01', 'R2', @AzurePlatformId, 'Realtime Judging Board', 'Realtime score board for judges.', 'https://github.com/example/realtime-judging', 'https://example.com/judging-demo', 'https://example.com/judging-doc', 22.10, 2, 'KhoaHLD', 'Reviewed', 1, 0, '2026-07-01T09:10:00', '2026-07-01T09:30:00'),
        ('TEAM02', 'R1', @VercelPlatformId, 'Mentor Matching App', 'Application for matching teams with mentors.', 'https://github.com/example/mentor-matching', 'https://example.com/mentor-demo', 'https://example.com/mentor-doc', 9.40, 1, 'KhoaHLD', 'Submitted', 1, 0, '2026-07-01T09:15:00', '2026-07-01T09:15:00'),
        ('TEAM02', 'R2', @AzurePlatformId, 'IoT Energy Monitor', 'IoT dashboard for energy tracking.', 'https://github.com/example/iot-energy-monitor', 'https://example.com/iot-demo', 'https://example.com/iot-doc', 31.00, 2, 'KhoaHLD', 'Reviewed', 0, 0, '2026-07-01T09:20:00', '2026-07-01T09:35:00'),
        ('TEAM03', 'R1', @VercelPlatformId, 'Campus Food Finder', 'Search application for campus food stalls.', 'https://github.com/example/campus-food-finder', 'https://example.com/food-demo', 'https://example.com/food-doc', 7.80, 1, 'KhoaHLD', 'Submitted', 1, 0, '2026-07-01T09:25:00', '2026-07-01T09:25:00'),
        ('TEAM03', 'R2', @AzurePlatformId, 'Healthcare Triage Bot', 'Bot for simple triage workflows.', 'https://github.com/example/healthcare-triage', 'https://example.com/health-demo', 'https://example.com/health-doc', 16.20, 2, 'KhoaHLD', 'Reviewed', 1, 1, '2026-07-01T09:30:00', '2026-07-01T09:45:00'),
        ('TEAM04', 'R1', @VercelPlatformId, 'Green Route Planner', 'Route planning app with carbon estimate.', 'https://github.com/example/green-route-planner', 'https://example.com/route-demo', 'https://example.com/route-doc', 14.65, 1, 'KhoaHLD', 'Submitted', 0, 0, '2026-07-01T09:35:00', '2026-07-01T09:35:00'),
        ('TEAM04', 'R2', @AzurePlatformId, 'Secure Voting Demo', 'Secure voting prototype for hackathon judging.', 'https://github.com/example/secure-voting', 'https://example.com/voting-demo', 'https://example.com/voting-doc', 20.00, 2, 'KhoaHLD', 'Reviewed', 1, 0, '2026-07-01T09:40:00', '2026-07-01T09:55:00'),
        ('TEAM05', 'R1', @VercelPlatformId, 'AI Feedback Analyzer', 'AI analyzer for mentor feedback.', 'https://github.com/example/ai-feedback-analyzer', 'https://example.com/feedback-demo', 'https://example.com/feedback-doc', 11.35, 1, 'KhoaHLD', 'Submitted', 1, 0, '2026-07-01T09:45:00', '2026-07-01T09:45:00');
END;

;WITH SeedNumbers AS (
    SELECT [item_no]
    FROM (VALUES
        (1), (2), (3), (4), (5), (6), (7), (8), (9), (10),
        (11), (12), (13), (14), (15), (16), (17), (18), (19), (20),
        (21), (22), (23), (24), (25), (26), (27), (28), (29), (30),
        (31), (32), (33), (34), (35), (36), (37), (38), (39), (40),
        (41), (42), (43), (44), (45), (46), (47), (48), (49), (50)
    ) AS v([item_no])
),
AdditionalProjectSubmissions AS (
    SELECT
        CASE
            WHEN [item_no] <= 10 THEN 'TEAM01'
            ELSE CONCAT('TEAM', RIGHT('00' + CAST((([item_no] - 11) % 15) + 6 AS varchar(2)), 2))
        END AS [team_id],
        CASE
            WHEN [item_no] <= 10 THEN 'R1'
            ELSE CONCAT('R', CAST((([item_no] - 11) % 4) + 1 AS varchar(1)))
        END AS [round_id],
        CASE
            WHEN [item_no] % 2 = 0 THEN @AzurePlatformId
            ELSE @VercelPlatformId
        END AS [platform_Khoahld_id],
        CASE
            WHEN [item_no] <= 10 THEN CONCAT('AI Paging Demo Project ', RIGHT('00' + CAST([item_no] AS varchar(2)), 2))
            WHEN [item_no] % 5 = 0 THEN CONCAT('AI Workflow Automation ', RIGHT('00' + CAST([item_no] AS varchar(2)), 2))
            WHEN [item_no] % 5 = 1 THEN CONCAT('Data Quality Dashboard ', RIGHT('00' + CAST([item_no] AS varchar(2)), 2))
            WHEN [item_no] % 5 = 2 THEN CONCAT('Cloud Deployment Monitor ', RIGHT('00' + CAST([item_no] AS varchar(2)), 2))
            WHEN [item_no] % 5 = 3 THEN CONCAT('Search Analytics Portal ', RIGHT('00' + CAST([item_no] AS varchar(2)), 2))
            ELSE CONCAT('Hackathon Review Toolkit ', RIGHT('00' + CAST([item_no] AS varchar(2)), 2))
        END AS [project_name],
        CASE
            WHEN [item_no] <= 10 THEN 'AI sample project for validating search with paging.'
            WHEN [item_no] % 5 = 0 THEN 'AI workflow project for automation and review scenarios.'
            WHEN [item_no] % 5 = 1 THEN 'Data dashboard project for reporting and quality checks.'
            WHEN [item_no] % 5 = 2 THEN 'Cloud monitoring project for deployment tracking.'
            WHEN [item_no] % 5 = 3 THEN 'Search portal project for filtering and analytics.'
            ELSE 'Hackathon toolkit project for judging support.'
        END AS [project_description],
        CONCAT('https://github.com/example/asm01-extra-', RIGHT('00' + CAST([item_no] AS varchar(2)), 2)) AS [repository_url],
        CONCAT('https://example.com/asm01-extra-', RIGHT('00' + CAST([item_no] AS varchar(2)), 2), '-demo') AS [demo_url],
        CONCAT('https://example.com/asm01-extra-', RIGHT('00' + CAST([item_no] AS varchar(2)), 2), '-doc') AS [document_url],
        CAST(8.00 + ([item_no] * 0.73) AS decimal(10, 2)) AS [repository_size_mb],
        (([item_no] - 1) % 3) + 1 AS [version_number],
        'KhoaHLD' AS [submitted_by],
        CASE
            WHEN [item_no] % 4 = 0 THEN 'Reviewed'
            WHEN [item_no] % 4 = 1 THEN 'Submitted'
            WHEN [item_no] % 4 = 2 THEN 'Updated'
            ELSE 'Pending'
        END AS [submission_status],
        CAST(CASE WHEN [item_no] % 3 = 0 THEN 0 ELSE 1 END AS bit) AS [is_deployed],
        CAST(CASE WHEN [item_no] % 10 = 0 THEN 1 ELSE 0 END AS bit) AS [is_late_submission],
        DATEADD(MINUTE, [item_no] * 3, CAST('2026-07-02T08:00:00' AS datetime2)) AS [submitted_at],
        DATEADD(MINUTE, [item_no] * 5, CAST('2026-07-02T08:30:00' AS datetime2)) AS [updated_at]
    FROM SeedNumbers
)
INSERT INTO [ProjectSubmissionsKhoaHLD] (
    [team_id],
    [round_id],
    [platform_Khoahld_id],
    [project_name],
    [project_description],
    [repository_url],
    [demo_url],
    [document_url],
    [repository_size_mb],
    [version_number],
    [submitted_by],
    [submission_status],
    [is_deployed],
    [is_late_submission],
    [submitted_at],
    [updated_at]
)
SELECT
    additional.[team_id],
    additional.[round_id],
    additional.[platform_Khoahld_id],
    additional.[project_name],
    additional.[project_description],
    additional.[repository_url],
    additional.[demo_url],
    additional.[document_url],
    additional.[repository_size_mb],
    additional.[version_number],
    additional.[submitted_by],
    additional.[submission_status],
    additional.[is_deployed],
    additional.[is_late_submission],
    additional.[submitted_at],
    additional.[updated_at]
FROM AdditionalProjectSubmissions additional
WHERE NOT EXISTS (
    SELECT 1
    FROM [ProjectSubmissionsKhoaHLD] existing
    WHERE existing.[project_name] = additional.[project_name]
);
