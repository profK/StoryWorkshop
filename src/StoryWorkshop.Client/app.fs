namespace StoryWorkshop.Client

open Bolero
open Microsoft.AspNetCore.Components
open Microsoft.JSInterop


type Application() =
    inherit Component()
    
     [<Inject>]
     member val JSRuntime = Unchecked.defaultof<IJSInProcessRuntime> with get, set
     
     abstract member Init:Unit
     abstract member Update:Unit
     abstract member Render:Unit
     
     override this.OnAfterRender(firstRender) =
          