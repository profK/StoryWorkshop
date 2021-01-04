module StoryWorkshop.Client.Main

open Bolero
open Microsoft.AspNetCore.Components
open Microsoft.JSInterop
open Bolero.Html


type MyApp() =
    inherit Component() 

    [<Inject>]
     member val JSRuntime = Unchecked.defaultof<IJSInProcessRuntime> with get, set

    override this.Render() =
        div [][]

    override this.OnAfterRender(firstRender) = 
        //Build the interface
        
    
   
    
