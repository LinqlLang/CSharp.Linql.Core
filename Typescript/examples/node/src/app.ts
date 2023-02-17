
import { LinqlContext, LinqlSearch } from "linql.client";
import { State } from "../../DataModel";

class LinqlNodeExample
{
    context = new LinqlContext(LinqlSearch, "https://localhost:7113", { this: this });


    async Run()
    {
        console.log('hello');
        // const states = this.context.Set<State>(State)
        // const firstState = await states.FirstOrDefaultAsync();
        // console.log(firstState.State_Name);
    }

}

export async function Main(): Promise<any>
{
    const example = new LinqlNodeExample();
    await example.Run();

}