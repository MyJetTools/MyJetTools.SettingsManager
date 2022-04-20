using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Template;
using MyJetTools.SettingsManager.Abstractions;

namespace MyJetTools.SettingsManager.Controllers;

public class SettingsController : ControllerBase
{
    private readonly ISettingsTemplateRepository _settingsTemplateRepository;
    private readonly TemplateMatcher _templateMatcher;
    
    public SettingsController(ISettingsTemplateRepository settingsTemplateRepository, TemplateMatcher templateMatcher)
    {
        _settingsTemplateRepository = settingsTemplateRepository;
        _templateMatcher = templateMatcher;
    }

    [HttpGet("settings/{env}/{serviceName}")]
    public async Task<IActionResult> GetSettings(string env, string serviceName)
    {
        if (string.IsNullOrEmpty(env) || string.IsNullOrEmpty(serviceName))
        {
            return BadRequest();
        }
        
        try
        {
            var template = await _settingsTemplateRepository.GetSettingsTemplateAsync(env, serviceName);

            if (template == null)
            {
                return NotFound();
            }
            
            return Ok(await _templateMatcher.MatchTemplate(template.SettingsYamlTemplate));
        }
        catch (Exception e)
        {
            return StatusCode(500);
        }
        

    }
}