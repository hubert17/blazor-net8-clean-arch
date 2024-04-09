# CLASSIFIED EMAIL FILTER

You have been hired by a company to write software to detect potential classified mail being exchanged in non-secure email accounts. Your software will act as a filter to test the incoming email text, detect if the email might be classified, and additionally replace any sensitive text with censored ***** characters. 

Please write the core code as a function that accepts as parameters: 
-	a list of classified words/phrases 
-	the email text 

The function should return:
-	`true/false flag` - If any of the classified words or phrases were located in the email 
-	`censored text` – a new email text where the classified words or phrases have been replaced with asterisks, or the original email text if there was no classified material in the email

## Code solution

    public record FilterClassifiedEmailCmd(string[] ClassifiedWords, string EmailText) : IRequest<ClassifiedEmailFilterDto>;

    public record ClassifiedEmailFilterDto(bool IsClassified, string CensoredText);

    public class FilterClassifiedEmailHandler : IRequestHandler<FilterClassifiedEmailCmd, ClassifiedEmailFilterDto>
    {
        public Task<ClassifiedEmailFilterDto> Handle(FilterClassifiedEmailCmd request, CancellationToken cancellationToken)
        {
            var result = FilterClassifiedEmail(request.ClassifiedWords!, request.EmailText!);
    
            return Task.FromResult(result);
        }
    
        private ClassifiedEmailFilterDto FilterClassifiedEmail(string[] classifiedWords, string emailText)
        {
            // Tokenize the email text
            var tokens = emailText.Split(new[] { ' ', '.', ',', ';', ':', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
    
            // Convert classified words/phrases to lowercase for case-insensitive comparison
            var lowerClassifiedWords = classifiedWords.Select(word => word.ToLower()).ToHashSet();
    
            // Check if any of the classified words/phrases exist as whole words in the email text (case-insensitive)
            var isClassified = tokens.Any(token => lowerClassifiedWords.Contains(token.ToLower()));
    
            // Replace classified words/phrases with asterisks (case-insensitive)
            var censoredText = emailText;
            foreach (var word in classifiedWords)
            {
                var lowerWord = word.ToLower();
                var tokenizedWord = $@"\b{lowerWord}\b"; // Ensure whole word match using word boundary \b
                censoredText = Regex.Replace(censoredText, tokenizedWord, new string('*', word.Length), RegexOptions.IgnoreCase);
            }
    
            return new ClassifiedEmailFilterDto(isClassified, censoredText);
        }
    }

        // POST: ClassifiedEmailController/Create
        [HttpPost]
        public async Task<ActionResult> Create(FilterClassifiedEmailCmd request)
        {
            return Ok(await _mediator.Send(request));
        }

## Test input
Request Body

    {
      "classifiedWords": ["motherfucker", "putang-ina", "slim"],
      "emailText": "That slimy motherfucker denied saying this. Well, whoever he is, PUTANG-INA s'ya!"
    }

Response Body

    {
      "isClassified": true,
      "censoredText": "That slimy ************ denied saying this. Well, whoever he is, ********** s'ya!"
    }
