try {
    Write-Host "Testing Balance API endpoint..."
    $response = Invoke-WebRequest -Uri "http://localhost:5150/api/balance" -Method GET -UseBasicParsing
    Write-Host "Status Code: $($response.StatusCode)"
    Write-Host "Response Content:"
    Write-Host $response.Content
} catch {
    Write-Host "Error: $($_.Exception.Message)"
    if ($_.Exception.Response) {
        Write-Host "Status Code: $($_.Exception.Response.StatusCode)"
        $reader = New-Object System.IO.StreamReader($_.Exception.Response.GetResponseStream())
        $responseBody = $reader.ReadToEnd()
        Write-Host "Response Body: $responseBody"
    }
}