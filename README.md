<h1>Lightweight dependency injector</h1>

<h3> How to use:</h3>
<ol>
  <li>First create a gameobject and put the injector script onto the gameobject</li>
  <li>Next in your objects class inherit IDependencyProvider and mark any methods you would like to provide with the [Provider] attribute</li>
  <li>Lastly inject Objects into test cases or other dependencies with the [Inject] attribute</li>
</ol>
<br>
<p>This code was greatly inspired by adammyhre Dependency Injector Repo</p>
